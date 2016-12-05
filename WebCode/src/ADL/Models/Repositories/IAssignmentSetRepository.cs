using System.Collections.Generic;

namespace ADL.Models.Repositories
 {

    public interface IAssignmentSetRepository {
        IEnumerable<AssignmentSet> AssignmentSets { get; }
        void SaveAssignmentSet(AssignmentSet assignmentSet);
        AssignmentSet DeleteAssignmentSet(int assignmentSetId);
    }
}
