using Transcriber.Models;

namespace Transcriber.Interfaces
{
    public interface ITranscriptionRepository
    {
        Task AddAsync(TranscriptionRequest request, CancellationToken cancellationToken);
    }
}
