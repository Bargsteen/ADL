#region Libraries

using System;
using ADLApp.Models;
using ADLApp.ViewModel;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class ExclusiveSolvePage
    {
        private readonly IAnswerSender answerSender = new RequestManager();

        public ExclusiveSolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            BindingContext = currentAssignment;
            AssignmentToSolve = currentAssignment;
        }

        public Assignment AssignmentToSolve { get; set; }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (answerOptionView?.SelectedItem == null)
            {
                await DisplayAlert("Fejl i besvaring", "Vælg venligst et svar", "OK");
                return;
            }
            SendAnswerButton.IsEnabled = false;
            int selectedAnswerIndex =
                AssignmentToSolve.AnswerOptions.IndexOf(answerOptionView.SelectedItem as AnswerOption);
            string status = await
                answerSender.SendAnswer(new Answer(AssignmentToSolve.AssignmentId)
                {
                    ChosenAnswer = selectedAnswerIndex
                });
            await
                Navigation.PushModalAsync(
                    new ExclusiveResultPage(new ExclusiveResultViewModel(AssignmentToSolve,
                        selectedAnswerIndex)));
            SendAnswerButton.IsEnabled = true;
            await Navigation.PopAsync();
        }
    }
}