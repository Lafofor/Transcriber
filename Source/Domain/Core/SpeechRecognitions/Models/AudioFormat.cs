
namespace Core.SpeechRecognitions.Models;

/// <summary>
/// Аудиоформат
/// </summary>
/// <param name="extension">Расширение</param>
/// <param name="samplingFrequency">Частота дискретизации</param>
public struct AudioFormat(string extension, float samplingFrequency)
{
    /// <summary>
    /// Расширение
    /// </summary>
    public string Extension = extension;

    /// <summary>
    /// Частота дискретизации
    /// </summary>
    public float SamplingFrequency = samplingFrequency;
}
