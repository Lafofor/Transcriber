using Transcriber.Contracts;
using Transcriber.Interfaces;
using Transcriber.Models;

namespace Transcriber.Services
{
    public class TranscriptionService : ITranscriptionService
    {
        private readonly ITranscriptionRepository _repository;

        public TranscriptionService(ITranscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ProcessAsync(TranscriptionRequestDto requestDto, CancellationToken cancellationToken)
        {
            var transcriptionResult = $"Transcription for {requestDto.FileName} processed at {DateTime.UtcNow}";

            var transcriptionRequest = new TranscriptionRequest
            {
                FileName = requestDto.FileName,
                Body = requestDto.Body,
                TranscribedText = transcriptionResult
            };

            await _repository.AddAsync(transcriptionRequest, cancellationToken);

            return transcriptionResult;
        }
    }
}

