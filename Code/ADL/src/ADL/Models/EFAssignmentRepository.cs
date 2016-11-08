using System.Collections.Generic;

namespace ADL.Models {

    public class EFAssignmentRepository : IAssignmentRepository 
    {
        private ApplicationDbContext context;

        public EFAssignmentRepository(ApplicationDbContext ctx) 
        {
            context = ctx;
        }

        public IEnumerable<Assignment> Assignments => context.Assignments;
    }
}
