using CloudContact.Api.Implementations.RegularCallRecordings;
using CloudContact.Api.Types;

namespace CloudContact.Api.Tests;

public sealed class FactCloudContactClientTests
{
    /// <summary>
    /// Имя для входа через фактический сервис
    /// </summary>
    private const string UserName = "not-set";
    
    /// <summary>
    /// API-секрет для пользователя
    /// </summary>
    private const string Secret = "not-set";

    /// <summary>
    /// Хостинг
    /// </summary>
    private const string TenantUrl = "not-set";

    [Fact]
    public async Task AuthenticateAsync_NotValid()
    {
        var client = CloudContactClient.Create(TenantUrl);

        await Assert.ThrowsAsync<HttpRequestException>
        (
            () => client.AuthenticateAsync("Test", "Test")
        );
    }
    
    [Fact]
    public async Task AuthenticateAsync_Valid()
    {
        var client = CloudContactClient.Create(TenantUrl);

        await client.AuthenticateAsync(UserName, Secret);
        
        Assert.True(client.Authenticated);
    }
}