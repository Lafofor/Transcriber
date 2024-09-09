using Core.Pipelines;
using OllamaTextProcessors.Models;
using OllamaTextProcessors.Types;

namespace OllamaTextProcessors.Extensions;

public static class RecognitionPipelineBuilderExtensions
{
    public static IRecognitionPipelineBuilder AddOllamaTextProcessor
    (
        this IRecognitionPipelineBuilder builder,
        OllamaApiSettings settings,
        HttpClient? httpClient = null
    )
    {
        var module = new OllamaTextProcessor(settings, httpClient);
            
        return builder.AddAsyncTextProcessor(module);
    }
    
    public static IRecognitionPipelineBuilder AddOllamaTextProcessor
    (
        this IRecognitionPipelineBuilder builder,
        string modelName,
        string baseAddress,
        string promptFormatWithPlaceHolder,
        HttpClient? httpClient = null
    )
    {
        var settings = new OllamaApiSettings()
        {
            Model = modelName,
            PromptFormatWithPlaceHolder = promptFormatWithPlaceHolder,
            BaseAddress = baseAddress
        };
        
        var module = new OllamaTextProcessor(settings, httpClient);
        
        return builder.AddAsyncTextProcessor(module);
    }
}