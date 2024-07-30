namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public class SpeechRecognitionConfig
{
    public AudioEncoding Encoding { get; set; }
    
    public int SampleRateHertz { get; set; }
    
    public int AudioChannelCount { get; set; }
    
    public bool EnableSeparateRecognitionPerChannel { get; set; }
    
    public string LanguageCode { get; set; }
    
    public List<string> AlternativeLanguageCodes { get; set; }
    
    public int MaxAlternatives { get; set; }
    
    public bool ProfanityFilter { get; set; }
    
    public SpeechAdaptation Adaptation { get; set; }
    
    public IEnumerable<SpeechContext> SpeechContexts { get; set; }
    
    public bool EnableWordTimeOffsets { get; set; }
    
    public bool EnableWordConfidence { get; set; }
    
    public bool EnableAutomaticPunctuation { get; set; }
    
    public bool EnableSpokenPunctuation { get; set; }
    
    public bool EnableSpokenEmojis { get; set; }
    
    public SpeakerDiarizationConfig DiarizationConfig { get; set; }
    
    public RecognitionMetadata Metadata { get; set; }
    
    public string Model { get; set; }
    
    public bool UseEnhanced { get; set; }
}