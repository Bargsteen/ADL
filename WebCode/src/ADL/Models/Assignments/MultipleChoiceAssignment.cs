using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADL.Models.Answers;

namespace ADL.Models.Assignments
{
    public class MultipleChoiceAssignment : Assignment
    {
        [Required(ErrorMessage = "Venligst skriv svarmulighederne")]
        public List<AnswerOption> AnswerOptions { get; set; }

        [Required(ErrorMessage = "Venligst vælg det/de korrekte svar")]
        public List<AnswerBool> AnswerCorrectness { get; set; }
    }
}
