using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        [Required(ErrorMessage = "Venligst indtast en svarmulighed")]
        public string AnswerOptions { get; set; }     
        [Required(ErrorMessage = "Venligst vælg det korrekte svar")]   
        public int CorrectAnswer { get; set; }
    }
}
