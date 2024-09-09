
namespace Core.TextProcessors;

/// <summary>
/// Интерфейс обработки текста
/// </summary>
public interface ITextProcessor
{
    /// <summary>
    /// Обработка текста
    /// </summary>
    /// <param name="text">Текст</param>
    /// <returns>Обработанный текст</returns>
    public string Process(string text);

    /// <summary>
    /// Обработка текста
    /// </summary>
    /// <param name="text">Текст</param>
    /// <returns>Обработанный текст</returns>
    public void Process(Span<char> text);
}
