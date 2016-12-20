namespace ADLApp.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AttachedAssignmentId { get; set; }
    }
}