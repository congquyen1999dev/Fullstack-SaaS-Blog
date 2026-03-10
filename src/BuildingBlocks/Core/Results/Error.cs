namespace BuildingBlocks.Core.Results;

// Represents an error that can occur during an operation, with a code, message, type, and optional metadata.
public sealed record Error(
   string? Code,
   string? Message,
   ErrorType? Type,
   IReadOnlyDictionary<string, object>? Metadata = null)
{
   public string? Code { get; } = Code;
   public string? Message { get; } = Message;
   public ErrorType? Type { get; } = Type;

   public override string ToString() => $"{Code}: {Message}";

   public static Error CreateError(string code, string message, ErrorType type = ErrorType.Unexpected,
      IReadOnlyDictionary<string, object>? metadata = null) => new(code, message, type, metadata);
   
   public static Error ValidationError(string code, string message) => new(code, message, ErrorType.Validation);
   public static Error NotFoundError(string code, string message) => new(code, message, ErrorType.NotFound);
   public static Error UnauthorizedError(string code, string message) => new(code, message, ErrorType.Unauthorized);
   public static Error ForbiddenError(string code, string message) => new(code, message, ErrorType.Forbidden);
   public static Error ConflictError(string code, string message) => new(code, message, ErrorType.Conflict);
   public static Error InternalServerError(string code, string message) => new(code, message, ErrorType.Internal);
}