using System.Collections.ObjectModel;

namespace BuildingBlocks.Core.Results;

//Factory live in here and Base-type.
public class Result
{
   protected Result(bool isSuccess, IEnumerable<Error>? errors)
   {
      var errorArray = errors?.ToArray() ?? [];

      switch (isSuccess)
      {
         case true when errorArray.Length > 0:
            throw new InvalidOperationException(
               "Success result cannot contain errors.");

         case false when errorArray.Length == 0:
            throw new InvalidOperationException(
               "Failure result must contain errors.");

         default:
            IsSuccess = isSuccess;
            Errors = errorArray;
            break;
      }
   }

   public bool IsSuccess { get; }

   public bool IsFailure => !IsSuccess;

   public IReadOnlyList<Error> Errors { get; }

   public static Result Success()
      => new(true, null);

   public static Result Failure(params Error[] errors)
      => new(false, errors);

   public static Result<T> Success<T>(T value)
      => new(value, true, null);

   public static Result<T> Failure<T>(params Error[] errors)
      => new(default, false, errors);

   public static Result<T> Failure<T>(IEnumerable<Error> errors)
      => new(default, false, errors);
}

//Pure data type and no static methods, only properties and constructor.
public sealed class Result<T> : Result
{
   private readonly T? _value;

   internal Result(T? value, bool isSuccess, IEnumerable<Error>? errors)
      : base(isSuccess, errors) => _value = value;

   public T Value =>
      IsSuccess ? _value! : throw new InvalidOperationException("Cannot access value of a failed result.");
}