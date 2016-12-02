using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class MultipleChoiceAssignment : Assignment
    {
        public List<int> CorrectAnswers { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
    }
}
