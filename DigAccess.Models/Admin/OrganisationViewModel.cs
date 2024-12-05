namespace DigAccess.Models.Admin
{
    public class OrganisationViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public OrgAdminViewModel OrgAdmin { get; set; }
    }
}
