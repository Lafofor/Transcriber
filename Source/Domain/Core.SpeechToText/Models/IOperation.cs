using Core.SpeechRecognitions.Models;
using Core.SpeechToText.Enums;

namespace Core.SpeechToText.Models;

public interface IOperation
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? DoneAt { get; set; }
    
    public Dictionary<string, string>? Metadata { get; set; }
    
    public OperationStatus Status { get; set; }
    
    public Speech? Result { get; set; }
}