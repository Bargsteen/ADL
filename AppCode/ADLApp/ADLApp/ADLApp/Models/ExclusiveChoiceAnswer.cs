using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class ExclusiveChoiceAnswer : Answer
    {
        public int ChosenAnswer { get; set; }
        public ExclusiveChoiceAnswer(int assignmentId) : base(assignmentId)
        {
        }
    }
}
