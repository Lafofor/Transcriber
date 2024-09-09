using Core.Pipelines;
using Core.SpeechToText.Services;
using Microsoft.Extensions.DependencyInjection;
using Pipeline.Implementations;
using Services.Implementations;

namespace Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTransriberService
    (
        this IServiceCollection services,
        Action<IRecognitionPipelineBuilder> configure
    )
    {
        var builder = new RecognitionPipelineBuilder();

        configure(builder);

        var service = new TranscriberService(builder.Build());

        return services.AddSingleton<ITranscriberService>(service);
    }
}