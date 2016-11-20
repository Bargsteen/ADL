using System;
using Xamarin.Forms;
using ADLApp.Models;

namespace ADLApp.Views
{
    public partial class SolvePage : ContentPage
    {
        public MultipleChoiceAssignment AssignmentToSolve { get; set; }
        public SolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            BindingContext = AssignmentToSolve;
            AssignmentToSolve = currentAssignment as MultipleChoiceAssignment;
            Title = AssignmentToSolve.Headline;
            Question.Text = AssignmentToSolve.Question;
            answerOptionView.ItemsSource = AssignmentToSolve.AnswerOptions;
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (AssignmentToSolve is MultipleChoiceAssignment)
            {
                int selectedAnswerIndex = AssignmentToSolve.AnswerOptions.IndexOf(answerOptionView.SelectedItem as AnswerOption);
                //Sends answer to backend
                await Navigation.PushModalAsync(new ResultPage(selectedAnswerIndex, AssignmentToSolve));
            }
            await Navigation.PopAsync();
        }
    }
}
