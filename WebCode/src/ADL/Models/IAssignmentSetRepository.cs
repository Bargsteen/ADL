using System.Collections.Generic;

namespace ADL.Models {

    public interface IAssignmentSetRepository {
        IEnumerable<AssignmentSet> AssignmentSets { get; }
        void SaveAssignmentSet(AssignmentSet assignmentSet);
        AssignmentSet DeleteAssignmentSet(int assignmentSetId);
    }
}
