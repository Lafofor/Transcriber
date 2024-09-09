namespace Core.TextProcessors;

public interface IAsyncTextProcessor
{
    /// <summary>
    /// Инициализирован
    /// </summary>
    public bool Initialized { get; }
    
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="token">Токен</param>
    /// <returns>Задача</returns>
    public Task InitAsync(CancellationToken token = default);
    
    /// <summary>
    /// Обработка текста
    /// </summary>
    /// <param name="text">Текст</param>
    /// <param name="token">Токен</param>
    /// <returns>Обработанный текст</returns>
    public Task<string> ProcessAsync(string text, CancellationToken token = default);
}