using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.Pipeline;

//Store in cache
public delegate Task<Result<TResponse>> CachedPipeline<in TRequest, TResponse>(IServiceProvider provider,
   TRequest request, CancellationToken ct);