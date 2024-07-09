using Transcriber.Models;
using Transcriber.Interfaces;

namespace Transcriber.Repositories
{
    public class TranscriptionRepository : ITranscriptionRepository
    {
        private readonly TranscriptionContext _context;

        public TranscriptionRepository(TranscriptionContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TranscriptionRequest request, CancellationToken cancellationToken)
        {
            await _context.TranscriptionRequests.AddAsync(request, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
