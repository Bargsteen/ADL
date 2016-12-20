#region Libraries

using System.Collections.Generic;

#endregion

namespace ADLApp.Models
{
    public class Assignment
    {
        public AssignmentType Type { get; set; }
        public int AssignmentId { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
        public int CorrectAnswer { get; set; }
        public List<AnswerBool> AnswerCorrectness { get; set; }
    }
}