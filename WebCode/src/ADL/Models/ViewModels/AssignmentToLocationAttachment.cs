using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class AssignmentToLocationAttachment
    {
        public int ChosenLocationId { get; set; }
        public int ChosenAssignmentId { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}