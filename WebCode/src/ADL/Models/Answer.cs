using System;
using System.Collections.Generic;

namespace ADL.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public List<string> ChosenAnswers { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public int AnsweredAssignmentSetId { get; set; }
        public string UserId { get; set; }
    }
}
