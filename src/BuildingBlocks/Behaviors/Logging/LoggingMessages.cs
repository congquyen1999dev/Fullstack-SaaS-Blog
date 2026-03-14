using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.Logging;

internal static partial class LoggingMessages
{
   [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "Handling {RequestName}")]
   public static partial void HandlingRequest(ILogger logger, string requestName);
   
   [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "{RequestName} completed in {TimeTaken}ms")]
   public static partial void RequestSucceeded(ILogger logger, string requestName, long timeTaken);
   
   [LoggerMessage(EventId = 1002, Level = LogLevel.Error, Message = "{RequestName} failed in {TimeTaken} with {ErrorCount} errors")]
   public static partial void RequestFailed(ILogger logger, string requestName, long timeTaken, int errorCount);
   
   [LoggerMessage(EventId = 1003, Level = LogLevel.Warning, Message = "{RequestName} took {TimeTaken}ms which is longer than expected")]
   public static partial void SlowRequestDetected(ILogger logger, string requestName, long timeTaken);
}