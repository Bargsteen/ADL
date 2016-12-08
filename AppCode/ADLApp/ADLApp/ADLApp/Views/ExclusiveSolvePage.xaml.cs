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
        public Assignment AssignmentToSolve { get; set; }
        public ExclusiveSolvePage(Assignment currentAssignment)
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
                answerSender.SendAnswer(new ExclusiveChoiceAnswer(AssignmentToSolve.AssignmentId) {ChosenAnswer = selectedAnswerIndex});
            await Navigation.PushModalAsync(new ExclusiveResultPage(new ExclusiveResultViewModel(AssignmentToSolve, selectedAnswerIndex)));
            SendAnswerButton.IsEnabled = true;
            await Navigation.PopAsync();
        }
    }
}
