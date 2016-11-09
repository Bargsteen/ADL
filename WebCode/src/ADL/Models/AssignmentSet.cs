using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.Models
{
    public class AssignmentSet
    {
        public string Headline { get; set; }
        public string Summary { get; set; }
        //public Teacher Author { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
