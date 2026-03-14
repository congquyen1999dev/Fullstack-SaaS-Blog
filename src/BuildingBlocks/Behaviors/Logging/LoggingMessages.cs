using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.Logging;

internal static partial class LoggingMessages
{
   [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "Handling {RequestName}")]
   public static partial void HandlingRequest(ILogger logger, string requestName);

   [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "{RequestName}")]
   public static partial void RequestSucceeded(ILogger logger, string requestName);

   [LoggerMessage(EventId = 1002, Level = LogLevel.Error, Message = "{RequestName} failed with {ErrorCount} errors")]
   public static partial void RequestFailed(ILogger logger, string requestName, int errorCount);
}