using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using static ADL.Models.EnumCollection;

namespace ADL.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public AssignmentType Type { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string AssignmentText { get; set; }
    }
}
