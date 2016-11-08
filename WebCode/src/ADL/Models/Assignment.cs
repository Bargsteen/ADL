using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.Models
{
    public class Assignment
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public int RightAnswer { get; set; }
    }
}
