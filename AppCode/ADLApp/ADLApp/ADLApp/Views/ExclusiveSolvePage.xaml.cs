using System;
using Xamarin.Forms;
using ADLApp.ViewModel;
using System.Net;
using ADL.Models;
using ADL.Models.Answers;
using ADL.Models.Assignments;

namespace ADLApp.Views
{
    public partial class ExclusiveSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        private readonly Assignment _assignmentToSolve;
        public ExclusiveSolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            BindingContext = currentAssignment;
            _assignmentToSolve = currentAssignment;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (answerOptionView?.SelectedItem == null)
            {
                await DisplayAlert("Fejl i besvaring", "Vælg venligst et svar", "OK");
                return;
            }
            SendAnswerButton.IsEnabled = false;
            int selectedAnswerIndex = _assignmentToSolve.AnswerOptions.IndexOf(answerOptionView.SelectedItem as AnswerOption);
            string status = await
                answerSender.SendAnswer(new Answer { AnsweredAssignmentId = _assignmentToSolve.AssignmentId,ChosenAnswer = selectedAnswerIndex});
            await Navigation.PushModalAsync(new ExclusiveResultPage(new ExclusiveResultViewModel(_assignmentToSolve, selectedAnswerIndex)));
            SendAnswerButton.IsEnabled = true;
            await Navigation.PopAsync();
        }
    }
}
