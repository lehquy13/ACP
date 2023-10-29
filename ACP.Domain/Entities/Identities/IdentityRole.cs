﻿using ACP.Domain.Common.Primitives;

namespace ACP.Domain.Entities.Identities;

public class IdentityRole : AggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
}