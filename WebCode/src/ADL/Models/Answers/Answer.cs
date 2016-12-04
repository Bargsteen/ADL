using System;
using static ADL.Models.EnumCollection;

namespace ADL.Models.Answers
{
    public abstract class Answer
    {
        public int AnswerId { get; set; }
        public AssignmentType Type { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
    }
}
