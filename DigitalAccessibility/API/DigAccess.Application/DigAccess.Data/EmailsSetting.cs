using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class EmailsSetting
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string IncomingServer { get; set; } = null!;

    public string OutgoingServer { get; set; } = null!;

    public int Port { get; set; }

    public virtual ICollection<BlindUsersEmail> BlindUsersEmails { get; set; } = new List<BlindUsersEmail>();
}
