using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class MultipleChoiceAnswer : Answer
    {
        public List<ChosenAnswerlBool> ChosenAnswers { get; set; }
        public MultipleChoiceAnswer(int assignmentId) : base(assignmentId)
        {
        }
    }
}
