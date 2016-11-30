using System;

namespace ADL.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int ChosenAnswerOption { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
    }
}
