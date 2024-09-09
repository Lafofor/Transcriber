using OllamaSharp;
using OllamaTextProcessors.Models;

namespace OllamaTextProcessors.Extensions;

public static class OllamaApiSettingsExtensions
{
    public static OllamaApiClient.Configuration ToConfiguration(this OllamaApiSettings settings)
    {
        return new OllamaApiClient.Configuration()
        {
            Model = settings.Model,
            Uri = new Uri(settings.BaseAddress)
        };
    }
}