namespace ACP.Results;

interface IResult
{
    bool IsSuccess { get; }

    bool IsFailure { get; }

    string DisplayMessage { get; }
}