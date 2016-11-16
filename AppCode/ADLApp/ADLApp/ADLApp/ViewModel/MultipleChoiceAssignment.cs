using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.ViewModel
{
    class MultipleChoiceAssignment : Assignment
    {
        public string AnswerOptionOne { get; set; }
        public string AnswerOptionTwo { get; set; }
        public string AnswerOptionThree { get; set; }
        public string AnswerOptionFour { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
