using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models; 

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage(int answer, MultipleChoiceAssignment answeredAssignment)
        {
            InitializeComponent();
            QuestionLabel.Text += answeredAssignment.Question;
            preTextLabel.Text = "Du svarede:";
            AnswerLabel.Text = answeredAssignment.AnswerOptions[answer];
            if (answer == answeredAssignment.CorrectAnswer)
            {
                FeedBackLabel.TextColor = Color.Green;
                FeedBackLabel.Text = "Godt gået, det er korrekt!";
            }
            else
            {
                FeedBackLabel.TextColor = Color.Red;
                FeedBackLabel.Text = $"Desværre, det rigtige svar var \"{answeredAssignment.AnswerOptions[answeredAssignment.CorrectAnswer]}\"";
            }
        }
    }
}
