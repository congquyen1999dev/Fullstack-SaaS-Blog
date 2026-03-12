using System.Collections.Concurrent;

using BuildingBlocks.Core.Pipeline;
using BuildingBlocks.Core.Results;

using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Core.CQRS.Requests;

public sealed class RequestDispatcher(IServiceProvider provider) : IRequestDispatcher
{
   private static readonly ConcurrentDictionary<Type, object> Pipelines = new();

   public Task<Result<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken ct)
      where TRequest : IRequest<TResponse>
   {
      var pipeline = (CachedPipeline<TRequest, TResponse>)Pipelines.GetOrAdd(
         typeof(TRequest),
         _ => BuildPipeline<TRequest, TResponse>());

      return pipeline(provider, request, ct);
   }

   private static CachedPipeline<TRequest, TResponse> BuildPipeline<TRequest, TResponse>()
      where TRequest : IRequest<TResponse>
   {
      return async (provider, request, ct) =>
      {
         var handler = provider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

         var behaviors = provider.GetServices<IPipelineBehavior<TRequest, TResponse>>().ToArray();

         RequestPipeline<TResponse> handlePipeline = () => handler.HandleAsync(request, ct);

         var pipeline = behaviors.Reverse().Aggregate(handlePipeline,
            (next, behavior) =>
               () => behavior.HandleAsync(request, next, ct));

         return await pipeline().ConfigureAwait(false);
      };
   }
}