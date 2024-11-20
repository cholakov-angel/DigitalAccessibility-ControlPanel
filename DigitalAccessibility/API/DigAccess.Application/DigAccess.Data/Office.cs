using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class Office
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid OrganisationId { get; set; }

    public string LocalPhone { get; set; } = null!;

    public int StreetNumber { get; set; }

    public Guid CityId { get; set; }

    public string Street { get; set; } = null!;

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual City City { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;
}
