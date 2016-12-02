using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class PersonAndAssignmentViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
    }
}