namespace ACP.Results;

public class Result : ResultBase
{
    private Result(bool isSuccess, Error error)
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

    #region implicit operators

    /// <summary>
    /// Implicitly convert from Error to Result
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result(Error error) => new(false, error);
    
    /// <summary>
    /// Implicitly convert from Exception to Result
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result(Exception error) => new(false, new Error("Unexpected error with exception", error.Message));

    #endregion

    public override Result WithError(Error error)
    {
        Errors.Add(error);
        return this;
    }
    
    public override Result WithError(Exception error)
    {
        Errors.Add(
            new Error("Unexpected error with exception", error.Message)
            );
        return this;
    }
}