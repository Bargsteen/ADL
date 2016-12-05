using System.Collections.Generic;

using ADL.Models.Assignments;
namespace ADL.Models
{
    public class AssignmentSetViewModel
    {
        public AssignmentSet AssignmentSet { get; set; }
        public List<TextAssignment> TextAssignments { get; set; }
        public List<MultipleChoiceAssignment> MultipleChoiceAssignments { get; set; }
        public List<ExclusiveChoiceAssignment> ExclusiveChoiceAssignments { get; set; }

    }

    public class AssignmentSetListViewModel
    {
        public IEnumerable<AssignmentSet> PublicAssignmentSets { get; set; }
        public IEnumerable<AssignmentSet> PrivateAssignmentSets { get; set; }
        public IEnumerable<AssignmentSet> InternalAssignmentSets { get; set; }
    }
}
