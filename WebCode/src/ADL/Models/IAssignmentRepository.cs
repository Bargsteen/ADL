using System.Collections.Generic;

namespace ADL.Models {

    public interface IAssignmentRepository {
        IEnumerable<Assignment> Assignments { get; }
        void SaveAssignment(Assignment EditedAssignment);
        Assignment DeleteAssignment(int assignmentId);

    }
}
