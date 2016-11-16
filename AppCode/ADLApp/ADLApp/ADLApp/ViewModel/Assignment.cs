using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.ViewModel
{
    public abstract class Assignment
    {
        public int AssignmentID { get; set; }
        public string Headline { get; set; }
        public string Question { get; set; }
    }
}
