using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public string Headline { get; set; }
        public string Question { get; set; }
        public string AnswerOptionOne { get; set; }
        public string AnswerOptionTwo { get; set; }
        public string AnswerOptionThree { get; set; }
        public string AnswerOptionFour { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
