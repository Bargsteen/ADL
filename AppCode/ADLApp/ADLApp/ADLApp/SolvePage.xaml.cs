using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADLApp.ViewModel;
using System.Collections.ObjectModel;
using Xamarin.Forms.Pages;

namespace ADLApp
{
    public partial class SolvePage : ContentPage
    {
        public string[] answerOptions { get; set; }
        public SolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            Title = currentAssignment.Headline;
            this.currentAssignment = currentAssignment;
            Question.Text = this.currentAssignment.Question;
            answerOptions = new string[] { currentAssignment.AnswerOptionOne,
                                           currentAssignment.AnswerOptionTwo,
                                           currentAssignment.AnswerOptionThree,
                                           currentAssignment.AnswerOptionFour};
            answerOptionView.ItemsSource = answerOptions;
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (Array.IndexOf(answerOptions, answerOptionView.SelectedItem) == currentAssignment.CorrectAnswer)
            {
                this.BackgroundColor = Color.Green;
            }
            else
            {
                this.BackgroundColor = Color.Red;
            }
        }
        public Assignment currentAssignment { get; set; }
    }
}
