using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class AnswerInformation
    {
        public string AnswerChosen { get; set; }
        public DateTime TimeAnswered { get; set; }
        public Assignment AnsweredAssignment { get; set; }
    }
}
