using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.CQRS.Requests;

public interface IRequestDispatcher
{
   Task<Result<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken ct)
      where TRequest : IRequest<TResponse>;
}