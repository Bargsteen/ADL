using System;
using System.Collections.Generic;
using static ADL.Models.EnumCollection;

namespace ADL.Models.Answers
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public AssignmentType Type { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
        public int ChosenAnswer { get; set; } // Exclusive
        public List<AnswerBool> ChosenAnswers { get; set; } // Multiple
        public string AnswerText { get; set; } // Text
    }
}
