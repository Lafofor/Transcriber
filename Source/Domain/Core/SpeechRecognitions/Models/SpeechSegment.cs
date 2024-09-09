
namespace Core.SpeechRecognitions.Models;

/// <summary>
/// Сегмент речи
/// </summary>
/// <param name="start">Начало</param>
/// <param name="end">Конец</param>
/// <param name="text">Текст</param>
public struct SpeechSegment(TimeSpan start, TimeSpan end, string text)
{
    /// <summary>
    /// Начало
    /// </summary>
    public readonly TimeSpan Start = start;

    /// <summary>
    /// Конец
    /// </summary>
    public readonly TimeSpan End = end;

    /// <summary>
    /// Текст
    /// </summary>
    public string Text = text;
}
