using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class MultipleChoiceAssignment : Assignment, INotifyPropertyChanged
    {
        public int AssignmentId { get; set; }
        public string Headline { get; set; }
        public string Question { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
        public int CorrectAnswer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
