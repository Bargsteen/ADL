#region Libraries

using System;
using ADLApp.Models;
using Xamarin.Forms;

#endregion

namespace ADLApp.ViewModel
{
    public class ExclusiveResultViewModel
    {
        public ExclusiveResultViewModel(Assignment assignment, int answer)
        {
            Assignment = assignment;
            CorrectAnswer = Assignment.AnswerOptions[Assignment.CorrectAnswer].Text;
            AnswerChosen = Assignment.AnswerOptions[answer].Text;
            if (CorrectAnswer == AnswerChosen)
                Feedback = new Tuple<string, Color>("Godt gået, det er korrekt!", Color.Green);
            else
                Feedback = new Tuple<string, Color>($"Desværre, det rigtige svar var \"{CorrectAnswer}\"",
                    Color.Red);
        }

        public Assignment Assignment { get; }
        public string AnswerChosen { get; }
        public Tuple<string, Color> Feedback { get; }
        public string CorrectAnswer { get; }
    }
}