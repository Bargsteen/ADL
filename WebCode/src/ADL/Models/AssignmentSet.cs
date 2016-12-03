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
        public string CreatorId { get; set; }
        public int SchoolId { get; set; }
        public DateTime DateOfCreation { get; set; }
        /*TEST ANDREAS*/
        public List<Assignment> Assignments = new List<Assignment>() { new Assignment() };
        //public List<Assignment> Assignments { get; set; }
    }
}
