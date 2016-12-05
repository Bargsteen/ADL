using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class MultipleChoiceAssignment : Assignment
    {
        public List<bool> AnswerCorrectness { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
    }
}
