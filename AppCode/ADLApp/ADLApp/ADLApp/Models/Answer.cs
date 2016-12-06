using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class Answer
    {
        public Answer(int assignmentId)
        {
            TimeAnswered = DateTime.Now;
            AnsweredAssignmentId = assignmentId;
        }
        public int AnswerId { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
    }
}
