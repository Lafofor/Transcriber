using System.Collections.Concurrent;
using System.Net;
using Core.TextProcessors.Abstractions;
using OllamaSharp;
using OllamaSharp.Models.Chat;
using OllamaTextProcessors.Extensions;
using OllamaTextProcessors.Models;

namespace OllamaTextProcessors.Types;

public sealed class OllamaTextProcessor : ApiTextProcessor
{
    private readonly OllamaApiSettings _settings;
    
    private readonly OllamaApiClient _apiClient;

    public OllamaTextProcessor(OllamaApiSettings apiSettings, HttpClient? httpClient) : base()
    {
        _settings = apiSettings;

        httpClient ??= new HttpClient()
        {
            BaseAddress = new Uri(_settings.BaseAddress),
            Timeout = TimeSpan.FromMinutes(30)
        };
        
        _apiClient = new OllamaApiClient( httpClient, _settings.Model );
    }

    public override Task InitAsync(CancellationToken token = default)
    {
        Initialized = true;
        
        return Task.CompletedTask;
    }

    public override async Task<string> ProcessAsync(string text, CancellationToken token = default)
    {
        // Создаем чат на один реквест

        var messageQueue = new ConcurrentQueue<string>();
        var tcs = new TaskCompletionSource();

        var chat = _apiClient.Chat
        (
            responseStream => ChatHandler(tcs, messageQueue, responseStream)
        );

        text = string.Format(_settings.PromptFormatWithPlaceHolder, text);

        await chat.Send(text, token);
        
        await tcs.Task;
        
        return string.Join
        (
            string.Empty,
            messageQueue
        );
    }

    private void ChatHandler(TaskCompletionSource tcs, ConcurrentQueue<string> queue, ChatResponseStream? responseStream)
    {
        var content = responseStream?.Message.Content;
        
        if ( content is not null )
            queue.Enqueue(content);
        
        if ( responseStream?.Done ?? true )
            tcs.SetResult();
    }
}