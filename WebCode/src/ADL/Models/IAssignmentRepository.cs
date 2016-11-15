using System.Collections.Generic;

namespace ADL.Models {

    public interface IAssignmentRepository {
        IEnumerable<Assignment> Assignments { get; }
        void SaveAssignment(Assignment EditedAssignment);
        void DeleteAssignment(Assignment assignment);

    }
}
