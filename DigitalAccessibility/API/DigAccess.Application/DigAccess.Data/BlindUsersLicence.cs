using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class BlindUsersLicence
{
    public Guid Id { get; set; }

    public Guid BlindUserId { get; set; }

    public string LicenceNumber { get; set; } = null!;

    public bool IsActivated { get; set; }

    public DateTime DateFrom { get; set; }

    public string? MacAddress { get; set; }

    public DateTime DateOfActivation { get; set; }

    public bool IsDeleted { get; set; }

    public virtual BlindUser BlindUser { get; set; } = null!;
}
