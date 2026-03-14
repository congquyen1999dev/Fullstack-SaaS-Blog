namespace BuildingBlocks.Behaviors.Performance;

public sealed class PerformanceOption
{
   public int SlowRequestThresholdMs { get; init; } = 500;

   public bool LogAllRequests { get; init; } = true;
}