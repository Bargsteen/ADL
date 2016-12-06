using System.Collections.Generic;

using ADL.Models.Assignments;
namespace ADL.Models.ViewModels
{
    public class AssignmentSetViewModel
    {
        public AssignmentSet AssignmentSet { get; set; }
        public List<Assignment> TextAssignments { get; set; }
        public List<Assignment> MultipleChoiceAssignments { get; set; }
        public List<Assignment> ExclusiveChoiceAssignments { get; set; }

    }

    public class AssignmentSetListViewModel
    {
        public IEnumerable<AssignmentSet> PublicAssignmentSets { get; set; }
        public IEnumerable<AssignmentSet> PrivateAssignmentSets { get; set; }
        public IEnumerable<AssignmentSet> InternalAssignmentSets { get; set; }
        public int CurrentSchoolId { get; set; }
    }
}
