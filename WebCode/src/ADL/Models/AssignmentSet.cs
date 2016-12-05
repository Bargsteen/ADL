using System;
using System.Collections.Generic;
using static ADL.Models.EnumCollection;
using ADL.Models.Assignments;
using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class AssignmentSet
    {
        public int AssignmentSetId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }       
        [Required]
        public PublicityLevel PublicityLevel { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public DateTime LastUpdateDate { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
