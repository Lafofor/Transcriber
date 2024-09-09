using Core.Pipelines;
using WhisperSpeechRecognition.Models;
using WhisperSpeechRecognition.Types;

namespace WhisperSpeechRecognition.Extensions;

public static class RecognitionPipelineBuilderExtensions
{
    public static IRecognitionPipelineBuilder UseWhisperRecognizer
    (
        this IRecognitionPipelineBuilder builder,
        WhisperSettings settings,
        string modelPath
    )
    {
        var module = new WhisperSpeechRecognizer(settings, modelPath);
        
        return builder.AddSpeechRecognizer(module);
    }
    
    public static IRecognitionPipelineBuilder UseWhisperRecognizer
    (
        this IRecognitionPipelineBuilder builder,
        string modelPath,
        string? language = null,
        int? threads = null,
        string? prompt = null
    )
    {
        var settings = new WhisperSettings()
        {
            Language = language ?? string.Empty,
            Threads = threads ?? 1,
            Prompt = prompt ?? string.Empty
        };
        
        var module = new WhisperSpeechRecognizer(settings, modelPath);
        
        return builder.AddSpeechRecognizer(module);
    }
}