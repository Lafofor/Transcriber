using System.ComponentModel.DataAnnotations.Schema;
using Core.SpeechRecognitions.Models;
using Core.SpeechToText.Enums;
using Core.SpeechToText.Models;

namespace Storage.EntityFrameworkCore.Models;

public sealed class Operation : IOperation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? DoneAt { get; set; }
 
    public OperationStatus Status { get; set; }

    internal string MetadataValue { get; set; } = null!;
    
    [NotMapped]
    Dictionary<string, string>? IOperation.Metadata { get; set; }
    
    [NotMapped]
    Speech? IOperation.Result { get; set; }
}