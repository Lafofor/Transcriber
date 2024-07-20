namespace CloudContact.Api.Interfaces;

/// <summary>
///     Интерфейс клиента к КлаудКонтакт
/// </summary>
public interface ICloudContactClient
{
    /*
     * В Api у них клиентский формат данных, который отправляем мы, x-www-form-urlencoded.
     */

    /// <summary>
    ///     Совершает запрос к КлаудКонтакт и возвращает сериализованное значение JSON
    /// </summary>
    /// <param name="apiMethod">Метод API</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">GET-параметры</param>
    /// <param name="bodyParameters">Параметры в теле</param>
    /// <param name="token">Токен</param>
    /// <typeparam name="TResult">Модель сериализации</typeparam>
    /// <returns>Сериализованнй ответ</returns>
    protected internal Task<TResult> MakeJsonRequestAsync<TResult>
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters = null,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters = null,
        CancellationToken token = default
    );

    /// <summary>
    ///     Совершает запрос к КлаудКонтакт и возвращает поток
    /// </summary>
    /// <param name="apiMethod">Метод API</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">GET-параметры</param>
    /// <param name="bodyParameters">Параметры в теле</param>
    /// <param name="token">Токен</param>
    /// <returns>Поток данных</returns>
    protected internal Task<Stream> MakeStreamRequestAsync
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters = null,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters = null,
        CancellationToken token = default
    );

    /// <summary>
    ///     Совершает запрос к КлаудКонтакт и возвращает поток
    /// </summary>
    /// <param name="apiMethod">Метод API</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">GET-параметры</param>
    /// <param name="bodyParameters">Параметры в теле</param>
    /// <param name="token">Токен</param>
    /// <returns>Строковое значение</returns>
    protected internal Task<string> MakeStringRequestAsync
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters = null,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters = null,
        CancellationToken token = default
    );

    /// <summary>
    /// Выполняет аутентификацию клиента КлаудКонтакт
    /// </summary>
    /// <param name="userName">Username</param>
    /// <param name="secret">API ключ</param>
    /// <param name="token">Токен</param>
    /// <returns>Задача</returns>
    public Task AuthenticateAsync(string userName, string secret, CancellationToken token = default);
    
    /// <summary>
    /// Состояние аутентификации
    /// </summary>
    public bool Authenticated { get; }
}