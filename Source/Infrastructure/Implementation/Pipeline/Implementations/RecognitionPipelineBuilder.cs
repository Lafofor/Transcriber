using Core.AudioProcessors;
using Core.Pipelines;
using Core.SpeechRecognitions;
using Core.SpeechRecognitions.Models;
using Core.TextProcessors;

namespace Pipeline.Implementations;

public sealed class RecognitionPipelineBuilder : IRecognitionPipelineBuilder
{
    private readonly IList<IAudioProcessor> _audioProcessors = new List<IAudioProcessor>();

    private readonly IList<ITextProcessor> _textPreProcessors = new List<ITextProcessor>();
    
    private readonly IList<ITextProcessor> _textPostProcessors = new List<ITextProcessor>();

    private readonly IList<IAsyncTextProcessor> _asyncTextProcessors = new List<IAsyncTextProcessor>();

    private Func<Speech, string>? _speechFormatter;

    private ISpeechRecognizer? _speechRecognizer;

    public IRecognitionPipelineBuilder AddAudioProcessor(IAudioProcessor audioProcessor)
    {
        _audioProcessors.Add(audioProcessor);
        
        return this;
    }

    public IRecognitionPipelineBuilder UseSpeechFormatter(Func<Speech, string>? speechFormatter)
    {
        _speechFormatter = speechFormatter;

        return this;
    }
    
    public IRecognitionPipelineBuilder AddSpeechRecognizer(ISpeechRecognizer speechRecognizer)
    {
        _speechRecognizer = speechRecognizer;
        
        return this;
    }
    
    public IRecognitionPipelineBuilder AddTextPreProcessor(ITextProcessor textProcessor)
    {
        _textPreProcessors.Add(textProcessor);
        
        return this;
    }

    public IRecognitionPipelineBuilder AddTextPostProcessor(ITextProcessor textProcessor)
    {
        _textPostProcessors.Add(textProcessor);
        
        return this;
    }

    public IRecognitionPipelineBuilder AddAsyncTextProcessor(IAsyncTextProcessor textProcessor)
    {
        _asyncTextProcessors.Add(textProcessor);
        
        return this;
    }

    public IRecognitionPipeline Build()
    {
        ArgumentNullException.ThrowIfNull(_speechRecognizer);

        return new RecognitionPipeline
        (
            _audioProcessors,
            _textPreProcessors,
            _textPostProcessors,
            _asyncTextProcessors,
            _speechRecognizer,
            _speechFormatter
        );
    }
}