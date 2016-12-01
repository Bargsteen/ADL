using System;
using System.Collections.Generic;
using static ADL.Models.EnumCollection;

namespace ADL.Models
{
    public class AssignmentSet
    {
        public int AssignmentSetId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }       
        public PublicityLevel PublicityLevel { get; set; }
        public Person Creator { get; set; }
        public School School { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
