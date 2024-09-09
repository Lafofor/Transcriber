using Core.AudioProcessors;
using Core.Pipelines;
using Core.SpeechRecognitions;
using Core.SpeechRecognitions.Models;
using Core.TextProcessors;

namespace Pipeline.Implementations;

public sealed class RecognitionPipeline
(
    IEnumerable<IAudioProcessor> audioProcessors,
    IEnumerable<ITextProcessor> textPreProcessors,
    IEnumerable<ITextProcessor> textPostProcessors,
    IEnumerable<IAsyncTextProcessor> asyncTextProcessors,
    ISpeechRecognizer speechRecognizer,
    Func<Speech, string>? speechFormatter = null
) : IRecognitionPipeline
{
    private readonly IEnumerable<IAudioProcessor> _audioProcessors = audioProcessors;

    private readonly IEnumerable<ITextProcessor> _textPreProcessors = textPreProcessors;
    
    private readonly IEnumerable<ITextProcessor> _textPostProcessors = textPostProcessors;

    private readonly IEnumerable<IAsyncTextProcessor> _asyncTextProcessors = asyncTextProcessors;

    private readonly ISpeechRecognizer _speechRecognizer = speechRecognizer;

    public async Task<Speech> ProcessAsync(Stream audio, CancellationToken token = default)
    {
        foreach (var audioProcessor in _audioProcessors)
        {
            if (await audioProcessor.AvailableAsync(audio, token))
                audio = await audioProcessor.ProcessAsync(audio, token);
        }

        if (!_speechRecognizer.Initialized)
            await _speechRecognizer.InitAsync(token);
        
        var recognizedSpeech = await _speechRecognizer.TranscribeAsync(audio, token);

        LinkedList<SpeechSegment> handledSegments = new LinkedList<SpeechSegment>();

        foreach (var segment in recognizedSpeech.SpeechSegments)
        {
            string text = ProcessTextFromProcessors(_textPreProcessors, segment.Text);

            foreach (var processor in _asyncTextProcessors)
            {
                if (!processor.Initialized)
                    await processor.InitAsync(token);
            
                text = await processor.ProcessAsync(text, token);
            }

            text = ProcessTextFromProcessors(_textPostProcessors, text);

            var handledSegment = new SpeechSegment(segment.Start, segment.End, text);

            handledSegments.AddLast(handledSegment);
        }

        return new Speech(handledSegments);
    }

    private string ProcessTextFromProcessors(IEnumerable<ITextProcessor> textProcessors, string text)
    {
        Span<char> spannedText = stackalloc char[text.Length];
        
        text.CopyTo(spannedText);

        foreach (var processor in textProcessors)
            processor.Process(spannedText);

        return new string(spannedText);
    }
}