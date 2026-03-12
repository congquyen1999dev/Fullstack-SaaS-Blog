using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.CQRS.Requests;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
   Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken ct);
}