using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADL.Models.Answers;
using static ADL.Models.EnumCollection;

namespace ADL.Models.Assignments
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public AssignmentType Type { get; set; }
        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Text { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; } // For Exclusive and Multiple
        public int CorrectAnswer { get; set; } // For Exclusive
        public List<AnswerBool> AnswerCorrectness { get; set; } // For Multiple
    }
}
