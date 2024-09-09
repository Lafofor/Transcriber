using Core.SpeechRecognitions.Abstractions;
using Core.SpeechRecognitions.Models;
using Whisper.net;
using WhisperSpeechRecognition.Models;

namespace WhisperSpeechRecognition.Types;

/// <summary>
/// Транскрибация текста через модель Whisper
/// </summary>
/// <param name="whisperSettings">Настройки модели</param>
/// <param name="modelPath">Путь к модели</param>
public sealed class WhisperSpeechRecognizer(WhisperSettings whisperSettings, string modelPath) : 
    DeepLearnSpeechRecognizer(modelPath, PrimaryFormat), 
    IDisposable
{
    /// <summary>
    /// Основной поддерживаемый формат - 16kHz WAV
    /// </summary>
    private static readonly AudioFormat PrimaryFormat = new AudioFormat("wav", 16000.0f);
    
    private WhisperFactory? _factory;

    private WhisperProcessor? _processor;
    
    public override Task InitAsync(CancellationToken token = default)
    {
        _factory = WhisperFactory.FromPath(ModelPath);

        _processor = _factory
            .CreateBuilder()
            .WithPrompt(whisperSettings.Prompt)
            .WithThreads(whisperSettings.Threads)
            .WithLanguage(whisperSettings.Language)
            .Build();

        Initialized = true;
        
        return Task.CompletedTask;
    }

    public override async Task<Speech> TranscribeAsync(Stream audioStream, CancellationToken token = default)
    {
        var speechSegments = new LinkedList<SpeechSegment>();

        await foreach (var segment in _processor!.ProcessAsync(audioStream, token))
        {
            var speechSegment = new SpeechSegment(segment.Start, segment.End, segment.Text);

            speechSegments.AddLast(speechSegment);
        }

        return new Speech(speechSegments);
    }

    public void Dispose()
    {
        _factory?.Dispose();
        _processor?.Dispose();
    }
}