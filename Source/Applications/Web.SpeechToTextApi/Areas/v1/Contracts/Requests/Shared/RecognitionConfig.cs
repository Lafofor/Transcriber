using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public sealed class RecognitionConfig
{
    public AudioEncoding? Encoding { get; set; }
    
    public int? SampleRateHertz { get; set; }
    
    public int? AudioChannelCount { get; set; }
    
    public bool? EnableSeparateRecognitionPerChannel { get; set; }
    
    public string? LanguageCode { get; set; }
    
    public List<string?>? AlternativeLanguageCodes { get; set; }
    
    public int? MaxAlternatives { get; set; }
    
    public bool? ProfanityFilter { get; set; }
    
    public SpeechAdaptation? Adaptation { get; set; }
    
    public IEnumerable<SpeechContext>? SpeechContexts { get; set; }
    
    public bool? EnableWordTimeOffsets { get; set; }
    
    public bool? EnableWordConfidence { get; set; }
    
    public bool? EnableAutomaticPunctuation { get; set; }
    
    public bool? EnableSpokenPunctuation { get; set; }
    
    public bool? EnableSpokenEmojis { get; set; }
    
    public SpeakerDiarizationConfig? DiarizationConfig { get; set; }
    
    public RecognitionMetadata? Metadata { get; set; }
    
    public string? Model { get; set; }
    
    public bool? UseEnhanced { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AudioEncoding : byte
    {
        [EnumMember(Value = "ENCODING_UNSPECIFIED")]
        EncodingUnspecified,
        
        [EnumMember(Value = "LINEAR16")]
        Linear16,
        
        [EnumMember(Value = "FLAC")]
        Flac,
        
        [EnumMember(Value = "MULAW")]
        Mulaw,
        
        [EnumMember(Value = "AMR")]
        Amr,
        
        [EnumMember(Value = "AMR_WB")]
        AmrWb,
        
        [EnumMember(Value = "OGG_OPUS")]
        OggOpus,
        
        [EnumMember(Value = "SPEEX_WITH_HEADER_BYTE")]
        SpeexWithHeaderByte,
        
        [EnumMember(Value = "WEBM_OPUS")]
        WebmOpus
    }
    
    public sealed class SpeechAdaptation
    {
        public IEnumerable<PhraseSet> PhraseSets { get; set; }
        
        public IEnumerable<string?> PhraseSetReferences { get; set; }
        
        public IEnumerable<CustomClass> CustomClasses { get; set; }
        
        public AugmentedBackusNaurForm? Grammar { get; set; }
        
        public sealed class AugmentedBackusNaurForm
        {
            public IEnumerable<string?> Strings { get; set; }
        }
    }
    
    public sealed class SpeechContext
    {
        public IEnumerable<string?> Phrases { get; set; }
        
        public int? Boost { get; set; }
    }
    
    public sealed class SpeakerDiarizationConfig
    {
        public bool? EnableSpeakerDiarization { get; set; }
        
        public int? MinSpeakerCount { get; set; }
        
        public int? MaxSpeakerCount { get; set; }
    }
    
    public sealed class RecognitionMetadata
    {
        [JsonPropertyName("InteractionType")]
        public InteractionType? Type { get; set; }
        
        public int? IndustryNaicsCodeOfAudio { get; set; }
        
        [JsonPropertyName("MicrophoneDistance")]
        public MicrophoneDistance? Distance { get; set; }
        
        [JsonPropertyName("OriginalMediaType")]
        public OriginalMediaType? MediaType { get; set; }
        
        [JsonPropertyName("RecordingDeviceType")]
        public RecordingDeviceType? DeviceType { get; set; }

        public string? RecordingDeviceName { get; set; } = null!;

        public string? OriginalMimeType { get; set; } = null!;

        public string? AudioTopic { get; set; } = null!;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum InteractionType : byte
        {
            InteractionTypeUnspecified,
            Discussion,
            Presentation,
            PhoneCall,
            VoiceMail,
            ProfessionallyProduced,
            VoiceSearch,
            VoiceCommand,
            Dictation
        }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum MicrophoneDistance : byte
        {
            MicrophoneDistanceUnspecified,
            NearField,
            MidField,
            FarField
        }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum OriginalMediaType : byte
        {
            OriginalMediaTypeUnspecified,
            Audio,
            Video
        }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RecordingDeviceType : byte
        {
            RecordingDeviceTypeUnspecified,
            Smartphone,
            Pc,
            PhoneLine,
            Vehicle,
            OtherOutdoorDevice,
            OtherIndoorDevice
        }
    }
}