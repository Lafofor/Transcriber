using CloudContact.Api.Interfaces;
using CloudContact.Api.Models;

namespace CloudContact.Api.Implementations.RegularCallRecordings;

/// <summary>
/// Расширения по получению звонков (взаимодействий)
/// </summary>
public static class RegularCallRecordingsExtensions
{
    /// <summary>
    /// Возвращает метаданные для звонка
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="id">Код записи</param>
    /// <param name="stepId">Шаг взаимодействия</param>
    /// <param name="token">Токен</param>
    /// <returns></returns>
    public static async Task<MetadataResponse> GetInteractionMetadataAsync
    (
        this ICloudContactClient client,
        Guid id,
        Guid stepId,
        CancellationToken token = default
    )
    {
        var parameters = new Dictionary<string, string?>()
        {
            { "giid", id.ToString() },
            { "stepid", stepId.ToString() }
        };
        
        return await client.MakeJsonRequestAsync<MetadataResponse>
        (
            "recordings/metadata",
            HttpMethod.Get,
            parameters,
            null,
            token
            
        );
    }
    
    /// <summary>
    /// Возвращает аудиофайл в GSM формате
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="id">Код записи</param>
    /// <param name="stepId">Шаг взаимодействия</param>
    /// <param name="token">Токен</param>
    /// <returns>GSM аудиофайл</returns>
    public static async Task<Stream> GetRegularRecordingAudioFileAsync
    (
        this ICloudContactClient client,
        Guid id,
        Guid stepId,
        CancellationToken token = default
    )
    {
        var parameters = new Dictionary<string, string?>()
        {
            { "giid", id.ToString() },
            { "stepid", stepId.ToString() }
        };
        
        return await client.MakeStreamRequestAsync
        (
            "recordings/audio",
            HttpMethod.Get,
            parameters,
            null,
            token
            
        );
    }
}