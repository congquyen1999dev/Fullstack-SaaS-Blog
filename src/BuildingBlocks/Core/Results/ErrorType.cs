namespace BuildingBlocks.Core.Results;

// Type of error that can occur during an operation.
public enum ErrorType
{
   Validation,
   NotFound,
   Conflict,
   Unauthorized,
   Forbidden,
   Unexpected,
   Internal
}