using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class Answer
    {
        public int AnswerId { get; set; }
        public AnswerOption ChosenAnswerOption { get; set; }
        public DateTime TimeAnswered { get; set; }
        public Assignment AnsweredAssignment { get; set; }
    }
}
