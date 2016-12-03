using System.Collections.Generic;

namespace ADL.Models
{
    public class AssignmentSetViewModel
    {
        public AssignmentSet AssignmentSet { get; set; }
        public List<Assignment> TextAssignments { get; set; }
        public List<MultipleChoiceAssignment> MultipleChoiceAssignments { get; set; }
        public List<ExclusiveChoiceAssignment> ExclusiveChoiceAssignments { get; set; }

    }
}
