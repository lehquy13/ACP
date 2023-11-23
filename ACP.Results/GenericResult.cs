namespace ACP.Results;

public class Result<T> : ResultBase, IHasError where T : notnull
{
    private readonly T? _value;

    public T? Value =>
        IsSuccess ? _value : throw new InvalidOperationException("Cannot access value for failed result");

    private Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    /// <summary>
    /// Implicitly convert a value to its possible result.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(T? value) =>
        value is not null
            ? new Result<T>(value, true, Error.None)
            : new Result<T>(default, false, Error.NullValue);

    /// <summary>
    /// Static factory method to create a successful result.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Success(T value) => new(value, true, Error.None);

    /// <summary>
    /// Static factory method to create a successful result with a message.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Result<T> Success(T value, string message)
        => new(value, true, Error.None) { DisplayMessage = message };

    /// <summary>
    /// Static factory method to create a failed result with an error.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Fail(T value, Error error) => new(value, false, error);

    /// <summary>
    /// Static factory method to create a failed result with an error message.
    /// It calls Result.Fail(string errorMessage) method.
    /// Then the result is converted to Result<![CDATA[T]]> >implicitly.
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static Result<T> Fail(string errorMessage) => Result.Fail(errorMessage);

    /// <summary>
    /// A result object can be implicitly converted to Result<![CDATA[T]]>.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(Result result)
        => new(default, result.IsSuccess, result.Error);
}