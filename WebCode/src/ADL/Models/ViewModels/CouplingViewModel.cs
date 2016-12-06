using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class CouplingViewModel
    {
        public int SchoolId { get; set; }
        public AssignmentSet ChosenAssignmentSet { get; set; }
        public IEnumerable<AssignmentSet> AvailableAssignmentSets { get; set; }
    }
}
