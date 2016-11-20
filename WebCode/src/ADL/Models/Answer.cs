using System;

namespace ADL.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public AnswerOption ChosenAnswerOption { get; set; }
        public DateTime TimeAnswered { get; set; }
        public Assignment AnsweredAssignment { get; set; }
    }
}
