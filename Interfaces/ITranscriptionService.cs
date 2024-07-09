using Transcriber.Contracts;

namespace Transcriber.Interfaces
{
    public interface ITranscriptionService
    {
        Task<string> ProcessAsync(TranscriptionRequestDto requestDto, CancellationToken cancellationToken);
    }
}
