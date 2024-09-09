namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public class RecognitionAudio
{
    public string? EncodedContent { get; set; }
    
    public string? Uri { get; set; }
}