using Core.SpeechRecognitions.Models;

namespace Core.SpeechToText.Services;

public interface ITranscriberService
{
    public Task<Speech> TranscribeAudioAsync(byte[] audio, CancellationToken token = default);
}