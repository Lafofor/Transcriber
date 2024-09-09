namespace OllamaTextProcessors.Models;

public sealed class OllamaApiSettings
{
    public string Model { get; set; } = null!;

    public string BaseAddress { get; set; } = null!;

    public string PromptFormatWithPlaceHolder { get; set; } = null!;
}