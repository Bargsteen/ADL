using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class TextAnswer : Answer
    {
        public string Answer { get; set; }
        public TextAnswer(int assignmentId) : base(assignmentId)
        {
        }
    }
}
