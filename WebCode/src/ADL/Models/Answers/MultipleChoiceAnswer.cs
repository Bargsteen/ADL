using System.Collections.Generic;

namespace ADL.Models.Answers
{
    public class MultipleChoiceAnswer : Answer
    {
        public List<ChosenAnswerlBool> ChosenAnswers { get; set; }
    }
}
