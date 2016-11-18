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
        public string AnswerOptionOne { get; set; }
        public string AnswerOptionTwo { get; set; }
        public string AnswerOptionThree { get; set; }
        public string AnswerOptionFour { get; set; }
        public int CorrectAnswer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, string> AnswerOptions = new Dictionary<string, string>();
        public void LoadData()
        {
            AnswerOptions.Add("A: ", AnswerOptionOne);
            AnswerOptions.Add("B: ", AnswerOptionTwo);
            AnswerOptions.Add("C: ", AnswerOptionThree);
            AnswerOptions.Add("D: ", AnswerOptionFour);
        }
    }
}
