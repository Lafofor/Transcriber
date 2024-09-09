
namespace Core.SpeechRecognitions.Models;

/// <summary>
/// Модель речи
/// </summary>
/// <param name="speechSegments"></param>
public struct Speech(IEnumerable<SpeechSegment> speechSegments)
{
    /// <summary>
    /// Длительность речи
    /// </summary>
    private TimeSpan? _duration;

    /// <summary>
    /// Полный текст речи
    /// </summary>
    private string? _text;

    /// <summary>
    /// Сегменты речи
    /// </summary>
    public readonly IEnumerable<SpeechSegment> SpeechSegments = speechSegments;

    /// <summary>
    /// Общая длительность речи
    /// </summary>
    public TimeSpan Duration => _duration ??= SpeechSegments.Max(x => x.End);

    /// <summary>
    /// Полный текст
    /// </summary>
    public string Text => _text ??= string.Join("", SpeechSegments.Select(s => s.Text));
}
