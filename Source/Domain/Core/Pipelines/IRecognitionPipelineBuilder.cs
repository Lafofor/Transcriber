using Core.AudioProcessors;
using Core.SpeechRecognitions;
using Core.SpeechRecognitions.Models;
using Core.TextProcessors;

namespace Core.Pipelines;

public interface IRecognitionPipelineBuilder
{
    public IRecognitionPipelineBuilder AddAudioProcessor(IAudioProcessor audioProcessor);

    public IRecognitionPipelineBuilder AddSpeechRecognizer(ISpeechRecognizer speechRecognizer);

    public IRecognitionPipelineBuilder AddTextPreProcessor(ITextProcessor textProcessor);
    
    public IRecognitionPipelineBuilder AddTextPostProcessor(ITextProcessor textProcessor);

    public IRecognitionPipelineBuilder AddAsyncTextProcessor(IAsyncTextProcessor textProcessor);

    public IRecognitionPipeline Build();
}