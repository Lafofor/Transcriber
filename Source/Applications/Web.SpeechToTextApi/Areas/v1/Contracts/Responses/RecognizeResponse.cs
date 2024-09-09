using System.Text.Json.Serialization;

namespace Web.SpeechToTextApi.Areas.v1.Contracts.Responses;

public sealed class RecognizeResponse
{
    public IEnumerable<SpeechRecognitionResult> Results { get; set; } = null!;

    public string TotalBilledTime { get; set; } = null!;

    [JsonPropertyName("SpeechAdaptationInfo")]
    public SpeechAdaptationInfo AdaptationInfo { get; set; } = null!;

    public string RequestId { get; set; } = null!;
    
    public class SpeechAdaptationInfo
    {
        public bool AdaptationTimeout { get; set; }

        public string TimeoutMessage { get; set; } = null!;
    }
    
    public class SpeechRecognitionResult
    {
        public IEnumerable<SpeechRecognitionAlternative> Alternatives { get; set; } = null!;
        
        public int ChannelTag { get; set; }

        public string ResultEndTime { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;
        
        public class SpeechRecognitionAlternative
        {
            public string Transcript { get; set; } = null!;
            
            public int Confidence { get; set; }

            public IEnumerable<WordInfo> Words { get; set; } = null!;
            
            public class WordInfo
            {
                public string StartTime { get; set; } = null!;

                public string EndTime { get; set; } = null!;

                public string Word { get; set; } = null!;
                
                public int Confidience { get; set; }
                
                public bool SpeakerTag { get; set; }
            }
        }
    }
}