using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.Pipeline;

public interface IPipelineBehavior<in TRequest, TResponse>
{
   Task<Result<TResponse>> HandleAsync(TRequest request, RequestPipeline<TResponse> next, CancellationToken ct);
}