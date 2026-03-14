using System.Diagnostics;

using BuildingBlocks.Core.Pipeline;
using BuildingBlocks.Core.Results;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Behaviors.Performance;

public sealed class PerformanceBehavior<TRequest, TResponse>(
   ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
   IOptions<PerformanceOption> options) : IPipelineBehavior<TRequest, TResponse>
{
   public async Task<Result<TResponse>> HandleAsync(TRequest request, RequestPipeline<TResponse> next,
      CancellationToken ct)
   {
      ArgumentNullException.ThrowIfNull(next);

      var requestName = typeof(TRequest).Name;
      var result = await next().ConfigureAwait(false);
      var timer = Stopwatch.StartNew();

      var timeTaken = timer.ElapsedMilliseconds;

      var threshold = options.Value.SlowRequestThresholdMs;
      
      //Log only slow requests
      if (timeTaken > threshold)
      {
         PerformanceMessages.RequestTookLong(logger, requestName, timeTaken, threshold);
      }
      //Log all requests.
      else if (options.Value.LogAllRequests)
      {
         PerformanceMessages.RequestCompleted(logger, requestName, timeTaken);
      }

      return result;
   }
}