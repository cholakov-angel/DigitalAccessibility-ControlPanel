using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class City
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BlindUser> BlindUsers { get; set; } = new List<BlindUser>();

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();
}
