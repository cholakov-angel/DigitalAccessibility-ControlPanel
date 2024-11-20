using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class BlindUsersLog
{
    public Guid Id { get; set; }

    public Guid BlindUserId { get; set; }

    public string LogType { get; set; } = null!;

    public int LogCode { get; set; }

    public string LogText { get; set; } = null!;

    public DateTime DateTimeOfLog { get; set; }

    public virtual BlindUser BlindUser { get; set; } = null!;
}
