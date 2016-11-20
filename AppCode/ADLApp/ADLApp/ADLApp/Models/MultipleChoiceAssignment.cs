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
        public string[] AnswerOptions = new string[4];

        public void LoadData()
        {
            AnswerOptions[0] = AnswerOptionOne;
            AnswerOptions[1] = AnswerOptionTwo;
            AnswerOptions[2] = AnswerOptionThree;
            AnswerOptions[3] = AnswerOptionFour;
        }
    }
}
