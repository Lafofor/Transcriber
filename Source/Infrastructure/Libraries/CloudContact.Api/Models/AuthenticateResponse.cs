using System.Text.Json.Serialization;

namespace CloudContact.Api.Models;

/// <summary>
/// Результат аутентификации
/// </summary>
public sealed class AuthenticateResponse
{
    /// <summary>
    /// Токен доступа
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// Тип токена
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;
    
    /// <summary>
    /// Сайт
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = null!;
    
    /// <summary>
    /// Время жизни
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}