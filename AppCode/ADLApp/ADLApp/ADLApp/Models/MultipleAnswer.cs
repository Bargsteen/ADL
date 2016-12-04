using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class MultipleAnswer : Answer
    {
        public List<bool> CheckedAnswers { get; set; }
        public MultipleAnswer(int assignmentId) : base(assignmentId)
        {
        }
    }
}
