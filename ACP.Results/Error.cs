﻿namespace ACP.Results;

public record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provideed");

    public static implicit operator Result(Error error) => Result.Fail(error);
}