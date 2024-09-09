using CommandWrapper.Sox.Types;
using Core.Pipelines;

namespace SoxAudioProcessor.Extensions;

public static class RecognitionPipelineBuilderExtensions
{
    public static IRecognitionPipelineBuilder AddSoxAudioProcessor
    (
        this IRecognitionPipelineBuilder builder,
        string executablePath,
        Action<SoxCommand> configure
    )
    {
        var command = new SoxCommand(executablePath);

        configure(command);

        var processor = new Types.SoxAudioProcessor(command);
        
        return builder.AddAudioProcessor(processor);
    }
}