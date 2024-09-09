
namespace Core.AudioProcessors;

/// <summary>
/// Обработка аудио
/// </summary>
public interface IAudioProcessor
{
    /// <summary>
    /// Проверяет доступно ли преобразование
    /// </summary>
    /// <param name="audioStream">Входящий аудиопоток</param>
    /// <param name="token">Токен</param>
    /// <returns>Доступность</returns>
    public Task<bool> AvailableAsync(Stream audioStream, CancellationToken token = default);

    /// <summary>
    /// Обрабатывает аудиопоток
    /// </summary>
    /// <param name="audioStream">Аудиопоток для изменения</param>
    /// <param name="token">Токен</param>
    /// <returns>Измененный поток</returns>
    public Task<Stream> ProcessAsync(Stream audioStream, CancellationToken token = default);
}
