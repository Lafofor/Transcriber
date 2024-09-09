using Core.SpeechRecognitions.Models;

namespace Core.SpeechRecognitions;

/// <summary>
/// Интерфейс транскрибации речи
/// </summary>
public interface ISpeechRecognizer
{
    /// <summary>
    /// Поддерживаемые аудиоформаты
    /// </summary>
    public IEnumerable<AudioFormat> SupportedFormats { get; }

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
    /// Выполняет транскрибацию речи
    /// </summary>
    /// <param name="audioStream">Аудиопоток</param>
    /// <param name="token">Токен</param>
    /// <returns>Речь</returns>
    public Task<Speech> TranscribeAsync(Stream audioStream, CancellationToken token = default);
}
