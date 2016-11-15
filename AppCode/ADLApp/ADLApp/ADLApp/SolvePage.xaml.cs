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
        public Assignment currentAssignment { get; set; }
        public SolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            Title = currentAssignment.Headline;
            this.currentAssignment = currentAssignment;
            Question.Text = this.currentAssignment.Question;
            if (currentAssignment is MultipleChoiceAssignment)
            {
                MultipleChoiceAssignment MPCAssignment = currentAssignment as MultipleChoiceAssignment;
                answerOptions = new string[] { MPCAssignment.AnswerOptionOne,
                                           MPCAssignment.AnswerOptionTwo,
                                           MPCAssignment.AnswerOptionThree,
                                           MPCAssignment.AnswerOptionFour
            };
                answerOptionView.ItemsSource = answerOptions;
            }
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (currentAssignment is MultipleChoiceAssignment)
            {
                if (Array.IndexOf(answerOptions, answerOptionView.SelectedItem) == ((MultipleChoiceAssignment)currentAssignment).CorrectAnswer)
                {
                    this.BackgroundColor = Color.Green;
                }
                else
                {
                    this.BackgroundColor = Color.Red;

                }
            }
        }
    }
}
