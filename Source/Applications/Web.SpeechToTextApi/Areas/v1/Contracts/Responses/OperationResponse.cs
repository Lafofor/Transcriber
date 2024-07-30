namespace Web.SpeechToTextApi.Areas.v1.Contracts.Responses;

public sealed class OperationResponse
{
    public string Name { get; set; } = null!;
    
    public Dictionary<string, string>? Metadata { get; set; }
    
    public bool IsCompleted { get; set; }

    public Status? Error { get; set; } = null;

    public RecognizeResponse? Response { get; set; } = null;
    
    public class Status
    {
        public int Code { get; set; }

        public string Message { get; set; } = null!;

        public IEnumerable<Detail>? Details { get; set; } = null;

        public class Detail
        {
            public string Type { get; set; } = null!;

            public string Field { get; set; } = null!;

            public object Value { get; set; } = null!;
        }
    }
}