using System;
using Xamarin.Forms;
using ADLApp.Models;
using ADLApp.ViewModel;
using System.Net;

namespace ADLApp.Views
{
    public partial class SolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager("http://adlearning.azurewebsites.net/api");
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
            if(answerOptionView?.SelectedItem == null)
            {
                await DisplayAlert("Fejl i besvaring", "Vælg venligst et svar", "OK");
                return;
            }
            SendAnswerButton.IsEnabled = false;
            if (AssignmentToSolve is MultipleChoiceAssignment)
            {
                int selectedAnswerIndex = AssignmentToSolve.AnswerOptions.IndexOf(answerOptionView.SelectedItem as AnswerOption);
                var status = await answerSender.SendAnswer(new Answer()
                {
                    ChosenAnswerOption = answerOptionView.SelectedItem as AnswerOption,
                    AnsweredAssignment = AssignmentToSolve,
                    TimeAnswered = DateTime.Now,
                });
                await Navigation.PushModalAsync(new ResultPage(selectedAnswerIndex, AssignmentToSolve));
            }
            SendAnswerButton.IsEnabled = true;
            await Navigation.PopAsync();
        }
    }
}
