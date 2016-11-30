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
    public class ResultViewModel : INotifyPropertyChanged
    {
        public ResultViewModel(Assignment assignment, int answer)
        {
            Assignment = assignment;
            if (assignment is ExclusiveChoiceAssignment)
            {
                ExclusiveChoiceAssignment mp = assignment as ExclusiveChoiceAssignment;
                CorrectAnswer = mp.AnswerOptions[mp.CorrectAnswer].Text;
                AnswerChosen = mp.AnswerOptions[answer].Text;
                if (CorrectAnswer == AnswerChosen)
                {
                    Feedback = new Tuple<string, Color>("Godt gået, det er korrekt!", Color.Green);
                }
                else
                {
                    Feedback = new Tuple<string, Color>($"Desværre, det rigtige svar var \"{CorrectAnswer}\"", Color.Red);
                }
            }
        }
        public Assignment Assignment { get; set; }
        public string AnswerChosen { get; set; }
        public Tuple<string,Color> Feedback { get; set; }
        public string CorrectAnswer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
