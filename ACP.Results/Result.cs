namespace ACP.Results;

public class Result : ResultBase
{
    private Result(bool isSuccess, Error error)
        : base(isSuccess, error)
    {
    }

    /// <summary>
    /// Implicitly convert from Error to Result
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result(Error error) => new(false, error);

    public static Result Success() =>
        new(true, Error.None);

    public static Result Success(string message)
        => new(true, Error.None) { DisplayMessage = message };

    public static Result Fail(Error error) =>
        new(false, error);

    public static Result Fail(string errorMessage)
        => new(false, new Error("Unexpected error", errorMessage));

    public override Result WithError(Error error)
    {
        Errors.Add(error);
        return this;
    }
}