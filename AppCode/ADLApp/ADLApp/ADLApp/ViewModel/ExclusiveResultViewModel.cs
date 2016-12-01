using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models;
using Xamarin.Forms;

namespace ADLApp.ViewModel
{
    public class ExclusiveResultViewModel : INotifyPropertyChanged
    {
        public ExclusiveResultViewModel(ExclusiveChoiceAssignment assignment, int answer)
        {
            Assignment = assignment;
            CorrectAnswer = Assignment.AnswerOptions[Assignment.CorrectAnswer].Text;
            AnswerChosen = Assignment.AnswerOptions[answer].Text;
            if (CorrectAnswer == AnswerChosen)
            {
                Feedback = new Tuple<string, Color>("Godt gået, det er korrekt!", Color.Green);
            }
            else
            {
                Feedback = new Tuple<string, Color>($"Desværre, det rigtige svar var \"{CorrectAnswer}\"", Color.Red);
            }
        }
        public ExclusiveChoiceAssignment Assignment { get; set; }
        public string AnswerChosen { get; set; }
        public Tuple<string, Color> Feedback { get; set; }
        public string CorrectAnswer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
