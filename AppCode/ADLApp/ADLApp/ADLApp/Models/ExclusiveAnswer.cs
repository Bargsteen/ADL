using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class ExclusiveAnswer : Answer
    {
        public int ChosenAnswer { get; set; }
        public ExclusiveAnswer(int assignmentId) : base(assignmentId)
        {
        }
    }
}
