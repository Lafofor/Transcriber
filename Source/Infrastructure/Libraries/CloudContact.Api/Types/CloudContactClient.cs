using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using CloudContact.Api.Interfaces;
using CloudContact.Api.Models;

namespace CloudContact.Api.Types;

/// <summary>
/// Реализация клиента
/// </summary>
public sealed class CloudContactClient : ICloudContactClient
{
    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="tenantUrl">Хостинг</param>
    /// <param name="httpClient">HTTP-клиент</param>
    /// <returns></returns>
    public static ICloudContactClient Create(string tenantUrl, HttpClient? httpClient = null)
        => new CloudContactClient(tenantUrl, httpClient);
    
    /// <summary>
    /// Текущий формат API.
    /// На момент 07.2024 выглядит так https://:tenant_url/configapi/v2/
    /// </summary>
    private const string CurrentApiFormat = "https://{0}/configapi/v2/";

    /// <summary>
    /// Семафор для потокобезопасного изменения переменной <see cref="Authenticated"/>
    /// </summary>
    private readonly SemaphoreSlim _semaphore;

    /// <summary>
    /// HTTP-клиент
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Была ли проведена аутентификация
    /// </summary>
    private bool _authenticated;
    
    /// <summary>
    /// Хостинг
    /// </summary>
    private readonly string _tenantUrl;
    
    /// <summary>
    /// Внутренний конструктор
    /// </summary>
    /// <param name="tenantUrl">Хостинг</param>
    /// <param name="httpClient">HTTP-клиент</param>
    private CloudContactClient(string tenantUrl, HttpClient? httpClient = null)
    {
        _tenantUrl = tenantUrl;

        _semaphore = new SemaphoreSlim(1);
        
        _httpClient = httpClient ?? new HttpClient()
        {
            BaseAddress = new Uri
            (
                string.Format(CurrentApiFormat, tenantUrl)
            )
        };
    }

    /// <summary>
    /// Метод для отправки запроса и возврата ответа в JSON
    /// </summary>
    /// <param name="apiMethod">Метод API (controller/method)</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">Параметры в строке запроса</param>
    /// <param name="bodyParameters">Параметры тела</param>
    /// <param name="token">Токен</param>
    /// <typeparam name="TResult">Сериализованный результат</typeparam>
    /// <returns>Сериализованный результат</returns>
    /// <exception cref="ArgumentNullException">Не удалось десериализовать JSON</exception>
    public async Task<TResult> MakeJsonRequestAsync<TResult>
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters,
        CancellationToken token
    )
    {
        var response = await CreateRequestAndSendAsync
        (
            apiMethod,
            method,
            getParameters,
            bodyParameters,
            token
        );

        var result = await response.Content.ReadFromJsonAsync<TResult>(token);
        
        return result ?? throw new ArgumentNullException(nameof(result));
    }

    /// <summary>
    /// Метод для отправки запроса и возврата ответа как поток
    /// </summary>
    /// <param name="apiMethod">Метод API (controller/method)</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">Параметры в строке запроса</param>
    /// <param name="bodyParameters">Параметры тела</param>
    /// <param name="token">Токен</param>
    /// <returns>Поток</returns>
    public async Task<Stream> MakeStreamRequestAsync
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters,
        CancellationToken token
    )
    {
        var response = await CreateRequestAndSendAsync
        (
            apiMethod,
            method,
            getParameters,
            bodyParameters,
            token
        );

        return await response.Content.ReadAsStreamAsync(token);
    }

    /// <summary>
    /// Метод для отправки запроса и возврата ответа как строка
    /// </summary>
    /// <param name="apiMethod">Метод API (controller/method)</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">Параметры в строке запроса</param>
    /// <param name="bodyParameters">Параметры тела</param>
    /// <param name="token">Токен</param>
    /// <returns>Строка</returns>
    public async Task<string> MakeStringRequestAsync
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters,
        CancellationToken token
    )
    {
        var response = await CreateRequestAndSendAsync
        (
            apiMethod,
            method,
            getParameters,
            bodyParameters,
            token
        );

        return await response.Content.ReadAsStringAsync(token);
    }

    /// <summary>
    /// Выполняет аутентификацию
    /// </summary>
    /// <param name="userName">Имя пользователя (логин)</param>
    /// <param name="secret">Сгенерированный API-секрет</param>
    /// <param name="token">Токен</param>
    public async Task AuthenticateAsync(string userName, string secret, CancellationToken token = default)
    {
        var body = new Dictionary<string, string?>()
        {
            { "client_id", userName },
            { "client_secret", secret },
            { "scope", _tenantUrl },
            { "grant_type", "client_credentials" }
        };

        var authenticateData = await MakeJsonRequestAsync<AuthenticateResponse>
        (
            "oauth/token",
            HttpMethod.Post,
            null,
            body,
            token
        );

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
        (
            authenticateData.TokenType,
            authenticateData.AccessToken
        );

        await _semaphore.WaitAsync(token);
        
        _authenticated = true;

        _semaphore.Release();
    }

    public bool Authenticated => _authenticated;

    #region Вспомогательные методы
    
    /// <summary>
    /// Служебный метод для создания запроса и отправки через <see cref="HttpClient"/>
    /// </summary>
    /// <param name="apiMethod">Метод API (controller/method)</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">Параметры в строке запроса</param>
    /// <param name="bodyParameters">Параметры тела</param>
    /// <param name="token">Токен</param>
    /// <returns>HTTP-ответ от сервиса</returns>
    private async Task<HttpResponseMessage> CreateRequestAndSendAsync
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters,
        CancellationToken token = default
    )
    {
        var request = CreateRequestMessage(apiMethod, method, getParameters, bodyParameters);
        
        var response = await _httpClient.SendAsync(request, token);
        
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch(HttpRequestException ex)
        {
            if (ex.StatusCode != HttpStatusCode.Unauthorized) 
                throw;
            
            await _semaphore.WaitAsync(token);
        
            _authenticated = true;

            _semaphore.Release();

            throw;
        }

        return response;
    }
    
    /// <summary>
    /// Служебный метод для создания HTTP-запроса
    /// </summary>
    /// <param name="apiMethod">Метод API (controller/method)</param>
    /// <param name="method">HTTP-метод</param>
    /// <param name="getParameters">Параметры в строке запроса</param>
    /// <param name="bodyParameters">Параметры тела</param>
    /// <returns>HTTP-запрос</returns>
    private static HttpRequestMessage CreateRequestMessage
    (
        string apiMethod,
        HttpMethod method,
        IEnumerable<KeyValuePair<string, string?>>? getParameters,
        IEnumerable<KeyValuePair<string, string?>>? bodyParameters
    )
    {
        apiMethod = string.Concat
        (
            apiMethod,
            CreateGetParameters( getParameters?.Where(p => p.Value is not null).ToArray() )
        );

        var requestMessage = new HttpRequestMessage(method, apiMethod);

        requestMessage.Content = CreateBodyFromParameters( bodyParameters?.Where(p => p.Value is not null)?.ToArray() );

        return requestMessage;
    }

    /// <summary>
    /// Служебный метод для создания тела запроса в формате x-www-form-urlencoded
    /// </summary>
    /// <param name="parameters">Поля формы</param>
    /// <returns>Данные формы</returns>
    private static FormUrlEncodedContent? CreateBodyFromParameters(KeyValuePair<string, string?>[]? parameters)
    {
        return parameters is null ? null : new FormUrlEncodedContent(parameters);
    }
    
    /// <summary>
    /// Служебный метод для создания строки с параметрами (?arg1=val1&arg2=val2)
    /// </summary>
    /// <param name="getParameters">Параметры</param>
    /// <returns>Строка с параметрами</returns>
    private static string CreateGetParameters(KeyValuePair<string, string?>[]? getParameters)
    {
        if (getParameters is null)
            return string.Empty;
        
        var parameters = new StringBuilder();

        var firstParameter = getParameters.First();
        var nextParameters = getParameters
            .Skip(1)
            .Select(p => $"{p.Key}={p.Value}");
        
        parameters.Append($"?{firstParameter.Key}={firstParameter.Value}");

        parameters.AppendJoin("&", nextParameters);

        return parameters.ToString();
    }
    
    #endregion
}