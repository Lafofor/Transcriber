using Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests;

public class LongRunningRecognizeRequest
{
    public RecognitionConfig Config { get; set; } = null!;

    public RecognitionAudio Audio { get; set; } = null!;

    public TranscriptOutputConfig OutputConfig { get; set; } = null!;
}