using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.Performance;

internal static partial class PerformanceMessages
{
   [LoggerMessage(EventId = 2000, Level = LogLevel.Information,
      Message = "{RequestName} completed successfully in {TimeTaken}ms")]
   public static partial void RequestCompleted(ILogger logger, string requestName, long timeTaken);

   [LoggerMessage(EventId = 2001, Level = LogLevel.Warning,
      Message = "{RequestName} took {TimeTaken}ms which exceeds the threshold of {Threshold}ms")]
   public static partial void RequestTookLong(ILogger logger, string requestName, long timeTaken, int threshold);
}