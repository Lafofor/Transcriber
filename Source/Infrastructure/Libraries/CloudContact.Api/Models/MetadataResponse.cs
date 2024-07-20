using System.Text.Json.Serialization;

namespace CloudContact.Api.Models;

/// <summary>
/// Ответ с метаданными аудиофайла
/// </summary>
public sealed class MetadataResponse
{
    /// <summary>
    /// Время взаимодействия
    /// </summary>
    [JsonPropertyName("Start Time")]
    public string StartTime { get; set; } = null!;
    
    /// <summary>
    /// Код оператора
    /// </summary>
    [JsonPropertyName("Agent loginId")]
    public ulong AgentLoginId { get; set; }

    /// <summary>
    /// Имя оператора
    /// </summary>
    [JsonPropertyName("Agent First Name")]
    public string AgentFirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия оператора
    /// </summary>
    [JsonPropertyName("Agent Last Name")]
    public string AgentLastName { get; set; } = null!;

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [JsonPropertyName("Customer phone")]
    public string CustomerPhone { get; set; } = null!;

    /// <summary>
    /// Направление звонка
    /// </summary>
    [JsonPropertyName("Direction")]
    public string Direction { get; set; } = null!;

    /// <summary>
    /// Сервис
    /// </summary>
    [JsonPropertyName("Service")]
    public string Service { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Disposition")]
    public string Disposition { get; set; } = null!;

    /// <summary>
    /// Заметки
    /// </summary>
    [JsonPropertyName("Notes")]
    public string Notes { get; set; } = null!;
  
    /// <summary>
    /// Аудиоподпись
    /// </summary>
    [JsonPropertyName("Voice Signature")]
    public ulong VoiceSignature { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Flagged")]
    public ulong Flagged { get; set; }
    
    /// <summary>
    /// Время разговора
    /// </summary>
    [JsonPropertyName("Talk Time")]
    public TimeSpan TalkTime { get; set; }

    /// <summary>
    /// Код записи
    /// </summary>
    [JsonPropertyName("RecordingId")]
    public string RecordingId { get; set; } = null!;

    /// <summary>
    /// Глобальный код записи
    /// </summary>
    [JsonPropertyName("Global Interaction ID")]
    public Guid GlobalInteractionId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Review URL")]
    public string ReviewUrl { get; set; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Screen Recording")]
    public ulong ScreenRecording { get; set; }
}