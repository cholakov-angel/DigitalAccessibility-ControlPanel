using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class Feature
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsLicenceKeyRequired { get; set; }

    public virtual ICollection<BlindUsersFeature> BlindUsersFeatures { get; set; } = new List<BlindUsersFeature>();
}
