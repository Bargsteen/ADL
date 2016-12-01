using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class ExclusiveChoiceAssignment : Assignment
    {
        public List<AnswerOption> AnswerOptions { get; set; }

        [Required(ErrorMessage = "Venligst vælg det korrekte svar")]
        public int CorrectAnswer { get; set; }
    }
}
