using Microsoft.Extensions.DependencyInjection;
using Web.SpeechToTextApi.Areas.v1.Controllers;

namespace Web.SpeechToTextApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeechToTextApi(this IServiceCollection collection)
    {
        collection.AddControllers()
            .AddApplicationPart(typeof(OperationsController).Assembly);
        
        return collection;
    }
}