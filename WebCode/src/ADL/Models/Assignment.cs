using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Headline { get; set; }
        [Required(ErrorMessage = "Venligst indtast et spørgsmål")]
        [MinLengthAttribute(2)]
        public string Question { get; set; }
        [Required(ErrorMessage = "Venligst skriv mindst to svarmuligheder")]
        [MinLengthAttribute(1)]
        public string AnswerOptionOne { get; set; }
        [Required(ErrorMessage = "Venligst skriv mindst to svarmuligheder")]
        [MinLengthAttribute(1)]
        public string AnswerOptionTwo { get; set; }
        public string AnswerOptionThree { get; set; }
        public string AnswerOptionFour { get; set; }

        [Required(ErrorMessage = "Venligst vælg det korrekte svar")]   
        public int CorrectAnswer { get; set; }
    }
}
