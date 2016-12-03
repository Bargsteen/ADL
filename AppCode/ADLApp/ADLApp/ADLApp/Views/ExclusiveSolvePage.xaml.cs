using System;
using Xamarin.Forms;
using ADLApp.Models;
using ADLApp.ViewModel;
using System.Net;

namespace ADLApp.Views
{
    public partial class ExclusiveSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        public ExclusiveChoiceAssignment AssignmentToSolve { get; set; }
        public ExclusiveSolvePage(ExclusiveChoiceAssignment currentAssignment)
        {
            InitializeComponent();
            BindingContext = currentAssignment;
            AssignmentToSolve = currentAssignment;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (answerOptionView?.SelectedItem == null)
            {
                await DisplayAlert("Fejl i besvaring", "Vælg venligst et svar", "OK");
                return;
            }
            SendAnswerButton.IsEnabled = false;
            int selectedAnswerIndex = AssignmentToSolve.AnswerOptions.IndexOf(answerOptionView.SelectedItem as AnswerOption);
            string status = await
                answerSender.SendAnswer(new Answer(AssignmentToSolve.AssignmentId, selectedAnswerIndex.ToString()));
            await Navigation.PushModalAsync(new ExclusiveResultPage(new ExclusiveResultViewModel(AssignmentToSolve, selectedAnswerIndex)));
            SendAnswerButton.IsEnabled = true;
            await Navigation.PopAsync();
        }
    }
}
