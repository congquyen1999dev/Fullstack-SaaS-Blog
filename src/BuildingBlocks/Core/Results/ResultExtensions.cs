namespace BuildingBlocks.Core.Results;

public static class ResultExtensions
{
   //Merge multiple results into one.
   public static Result Combine(params Result[] results)
   {
      ArgumentNullException.ThrowIfNull(results);
      var errors = results.Where(r => r.IsFailure).SelectMany(r => r.Errors).ToArray();

      return errors.Length == 0 ? Result.Success() : Result.Failure(errors);
   }

   public static Result Combine<T>(params Result<T>[] results)
   {
      ArgumentNullException.ThrowIfNull(results);
      var errors = results.Where(r => r.IsFailure).SelectMany(r => r.Errors).ToArray();

      return errors.Length == 0 ? Result.Success() : Result.Failure(errors);
   }

   //Transforms value inside successful result.
   public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(mapper);

      return result.IsFailure ? Result.Failure<TOut>(result.Errors) : Result.Success(mapper(result.Value));
   }

   public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Result<TIn> result,
      Func<TIn, Task<TOut>> mapper)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(mapper);

      if (result.IsFailure)
         return Result.Failure<TOut>(result.Errors);

      var value = await mapper(result.Value).ConfigureAwait(false);

      return Result.Success(value);
   }

   //Used for chaining operations that return results.
   public static async Task<Result<TOut>> BindAsync<TIn, TOut>(this Result<TIn> result,
      Func<TIn, Task<Result<TOut>>> binder)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(binder);

      return result.IsFailure ? Result.Failure<TOut>(result.Errors) : await binder(result.Value).ConfigureAwait(false);
   }

   public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> binder)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(binder);

      return result.IsFailure ? Result.Failure<TOut>(result.Errors) : binder(result.Value);
   }

   //Execute side effects without changing the result.
   public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(action);

      if (result.IsSuccess) action(result.Value);

      return result;
   }

   //Validate conditions on the value. If predicate fails, convert to Failure with provided error.
   public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(predicate);

      if (result.IsFailure) return result;

      return !predicate(result.Value) ? Result.Failure<T>(error) : result;
   }

   //Convert a successful result to another type.
   public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> onSuccess,
      Func<IReadOnlyList<Error>, TOut> onFailure)
   {
      ArgumentNullException.ThrowIfNull(result);
      ArgumentNullException.ThrowIfNull(onSuccess);
      ArgumentNullException.ThrowIfNull(onFailure);

      return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors);
   }

   //Convert exceptions to result.
   public static Result<T> Try<T>(Func<T> func)
   {
      ArgumentNullException.ThrowIfNull(func);

      try
      {
         return Result.Success(func());
      }
      catch (Exception ex)
      {
         return Result.Failure<T>(Error.CreateError("Unexpected", ex.Message));
      }
   }
}