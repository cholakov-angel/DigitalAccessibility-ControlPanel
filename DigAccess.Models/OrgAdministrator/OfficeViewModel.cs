namespace DigAccess.Models.OrgAdministrator
{
    public class OfficeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int StreetNumber { get; set; }

        public string? CityName { get; set; }

        public string Street { get; set; } = null!;

        public WorkerViewModel? OfficeAdministrator { get; set; }
        public List<WorkerViewModel>? Workers { get; set; }
    }
}
