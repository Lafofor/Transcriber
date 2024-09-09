using System.Linq.Expressions;
using Core.SpeechToText.Models;

namespace Core.SpeechToText.Services;

public interface IOperationService
{
    public Task<IOperation> CreateOperationAsync(Stream audio, CancellationToken token = default);

    public Task<IEnumerable<IOperation>> ReadOperationsAsync
    (
        int page,
        int countOnPage,
        Expression<IOperation> filter,
        CancellationToken token = default
    );

    public Task CancelOperationAsync(IOperation operation, CancellationToken token = default);
    
    public Task StartOperationAsync(IOperation operation, CancellationToken token = default);
}