

namespace Core.TextProcessors.Abstractions;

/// <summary>
/// Обработка текста в стороннем API
/// </summary>
public abstract class ApiTextProcessor : IAsyncTextProcessor, IDisposable
{
    /// <summary>
    /// Http Client
    /// </summary>
    protected readonly HttpClient HttpClient = null!;
    
    protected ApiTextProcessor()
    { }

    public bool Initialized { get; protected set; }
    
    public abstract Task InitAsync(CancellationToken token = default);
    
    public abstract Task<string> ProcessAsync(string text, CancellationToken token = default);

    public virtual void Dispose()
        => HttpClient.Dispose();
}
