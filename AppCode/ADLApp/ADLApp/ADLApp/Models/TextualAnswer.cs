using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class TextualAnswer
    {
        public TextualAnswer(string answer, int assignmentId)
        {
            Answer = answer;
            TimeAnswered = DateTime.Now;
            AnsweredAssignmentId = assignmentId;
        }
        public int AnswerId { get; set; }
        public string Answer { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
    }
}
