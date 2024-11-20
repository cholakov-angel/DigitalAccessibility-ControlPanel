using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class BlindUsersEmail
{
    public Guid Id { get; set; }

    public Guid BlindUserId { get; set; }

    public string Email { get; set; } = null!;

    public string EmailPassword { get; set; } = null!;

    public Guid EmailSettingsId { get; set; }

    public virtual BlindUser BlindUser { get; set; } = null!;

    public virtual EmailsSetting EmailSettings { get; set; } = null!;
}
