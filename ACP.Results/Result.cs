namespace ACP.Results;

public class Result : ResultBase
{
    protected Result(bool isSuccess, Error error)
        : base(isSuccess, error)
    {
    }

    public static Result Success() =>
        new(true, Error.None);

    public static Result Success(string message)
        => new(true, Error.None) { DisplayMessage = message };

    public static Result Fail(Error error) =>
        new(false, error);

    public static Result Fail(string errorMessage)
        => new(false, new Error("Unexpected error", errorMessage));
}

public abstract class ResultBase : IResult
{
    public bool IsSuccess { get; private init; }

    public bool IsFailure => !IsSuccess;

    public string DisplayMessage { get; protected init; } = string.Empty;

    public Error Error { get; }

    //TODO: consider to remove this one
    public List<string> ErrorMessages { get; protected set; } = new();

    protected internal ResultBase(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new ArgumentException("Cannot supply error for successful result");
        }

        if (!IsSuccess && error == Error.None)
        {
            throw new ArgumentException("Must supply error for failed result");
        }

        IsSuccess = isSuccess;
        Error = error;
    }
}