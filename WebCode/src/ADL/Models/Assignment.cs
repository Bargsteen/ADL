using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ADL.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Headline { get; set; }
        [Required(ErrorMessage = "Venligst indtast et spørgsmål")]
        [MinLengthAttribute(2)]
        public string Question { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
        [Required(ErrorMessage = "Venligst vælg det korrekte svar")]   
        public int CorrectAnswer { get; set; }
    }
}
