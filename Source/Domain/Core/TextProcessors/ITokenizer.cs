
namespace Core.TextProcessors;

/// <summary>
/// Токенизатор текста
/// </summary>
internal interface ITokenizer
{
    /// <summary>
    /// Токенизирует текст
    /// </summary>
    /// <param name="text">Текст</param>
    /// <returns>Токены</returns>
    public IEnumerable<string> Tokenize(string text);

    /// <summary>
    /// Токенизирует текст
    /// </summary>
    /// <param name="text">Текст</param>
    /// <returns>Токены</returns>
    public IEnumerable<string> Tokenize(ReadOnlySpan<char> text);
}
