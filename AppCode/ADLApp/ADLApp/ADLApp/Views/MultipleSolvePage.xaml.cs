#region Libraries

using System;
using System.Collections.Generic;
using System.Linq;
using ADLApp.Models;
using ADLApp.ViewModel;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class MultipleSolvePage
    {
        private List<bool> answers = new List<bool>();
        private readonly IAnswerSender answerSender = new RequestManager();
        private readonly Assignment assignmentToSolve;
        private readonly List<AnswerOption> ChosenAnswers = new List<AnswerOption>();

        public MultipleSolvePage(Assignment mca)
        {
            InitializeComponent();
            TextSL.Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
            BindingContext = mca;
            assignmentToSolve = mca;
        }

        private void OnChecked(object sender, bool isChecked)
        {
            if (isChecked)
                ChosenAnswers.Add(
                    assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
            else
                ChosenAnswers.Remove(
                    assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            SendAnswerButton.IsEnabled = false;
            List<AnswerBool> chosenAnswers = new List<AnswerBool>();
            for (int i = 0; i < assignmentToSolve.AnswerOptions.Count; i++)
                chosenAnswers.Add(new AnswerBool {Value = false});
            foreach (var ca in ChosenAnswers)
                chosenAnswers[assignmentToSolve.AnswerOptions.IndexOf(ca)].Value = true;
            await
                answerSender.SendAnswer(new Answer(assignmentToSolve.AssignmentId)
                {
                    ChosenAnswers = chosenAnswers
                });
            await
                Navigation.PushModalAsync(
                    new MultipleResultPage(new MultipleResultViewModel(chosenAnswers, assignmentToSolve)));
            await Navigation.PopAsync();
            SendAnswerButton.IsEnabled = true;
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            answerOptionView.SelectedItem = null;
        }
    }
}