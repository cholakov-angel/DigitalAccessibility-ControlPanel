using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class BlindUsersFeature
{
    public Guid BlindUserId { get; set; }

    public Guid FeatureId { get; set; }

    public string LicenceKey { get; set; } = null!;

    public virtual BlindUser BlindUser { get; set; } = null!;

    public virtual Feature Feature { get; set; } = null!;
}
