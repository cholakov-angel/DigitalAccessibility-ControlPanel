using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class BlindUser
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PersonalId { get; set; }

    public string? Telknumber { get; set; }

    public DateTime? Birthdate { get; set; }

    public string AdministratorId { get; set; } = null!;

    public Guid CityId { get; set; }

    public int StreetNumber { get; set; }

    public string Street { get; set; } = null!;

    public int Gender { get; set; }

    public virtual AspNetUser Administrator { get; set; } = null!;

    public virtual ICollection<BlindUsersEmail> BlindUsersEmails { get; set; } = new List<BlindUsersEmail>();

    public virtual ICollection<BlindUsersFeature> BlindUsersFeatures { get; set; } = new List<BlindUsersFeature>();

    public virtual ICollection<BlindUsersLicence> BlindUsersLicences { get; set; } = new List<BlindUsersLicence>();

    public virtual ICollection<BlindUsersLog> BlindUsersLogs { get; set; } = new List<BlindUsersLog>();

    public virtual City City { get; set; } = null!;
}
