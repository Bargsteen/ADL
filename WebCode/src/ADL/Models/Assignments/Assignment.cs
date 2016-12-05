using System.ComponentModel.DataAnnotations;
using static ADL.Models.EnumCollection;

namespace ADL.Models.Assignments
{
    public abstract class Assignment
    {
        public int AssignmentId { get; set; }
        public AssignmentType Type { get; set; }

        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Text { get; set; }
    }
}
