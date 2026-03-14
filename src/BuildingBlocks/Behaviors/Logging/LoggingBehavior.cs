using System.Diagnostics;

using BuildingBlocks.Core.Pipeline;
using BuildingBlocks.Core.Results;

using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.Logging;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
   public async Task<Result<TResponse>> HandleAsync(TRequest request, RequestPipeline<TResponse> next, CancellationToken ct)
   {
      ArgumentNullException.ThrowIfNull(next);
      //Name of the request.
     var requestName = typeof(TRequest).Name;
     
     //Request start
     logger.LogInformation("Handing {RequestName}", requestName);
     
     var result = await next().ConfigureAwait(false);
     
     var timer = Stopwatch.StartNew();

     var timeTaken  = timer.ElapsedMilliseconds;

     if (!result.IsSuccess)
     {
        //Request failed
        logger.LogError("{RequestName} failed with {Errors}", requestName, result.Errors.Count);
     }
     else
     {
        //Request succeeded
        logger.LogInformation("{RequestName} completed successfuly", requestName);
        
        //Slow request detected.
        if (timeTaken > 3000)
        {
           logger.LogWarning("{RequestName} took {TimeTaken}ms which is longer than expected", requestName, timeTaken);
        }
     }

     return result;
   }
}