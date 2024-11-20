using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class Organisation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string NationalPhone { get; set; } = null!;

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();
}
