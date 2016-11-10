using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Model
{
    public class Assignment
    {
        public string Headline { get; }
        public string Question { get; }
        public string[] AnswerOptions { get; }
        public int CorrectAnswer { get; }
        public int AssignmentID { get; }
        public Assignment(string h, string q, string[] ao, int cA, int aID)
        {
            Headline = h;
            Question = q;
            AnswerOptions = ao;
            CorrectAnswer = cA;
            AssignmentID = aID;
        }
    }
}
