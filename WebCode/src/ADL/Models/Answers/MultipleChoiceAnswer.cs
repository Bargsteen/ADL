using System.Collections.Generic;

namespace ADL.Models.Answers
{
    public class MultipleChoiceAnswer : Answer
    {
        public List<AnswerBool> ChosenAnswers { get; set; }
    }
}
