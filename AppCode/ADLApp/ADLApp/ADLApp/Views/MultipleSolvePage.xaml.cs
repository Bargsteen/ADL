using ADL.Models;
using ADLApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.Answers;
using ADL.Models.Assignments;
using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class MultipleSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        List<AnswerOption> ChosenAnswers = new List<AnswerOption>();
        List<bool> answers = new List<bool>();
        public MultipleSolvePage(Assignment mca)
        {
            InitializeComponent();
            BindingContext = mca;
            assignmentToSolve = mca;
        }
        private Assignment assignmentToSolve;
        private void OnChecked(object sender, bool isChecked)
        {
            if (isChecked)
            {
                ChosenAnswers.Add(assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
            }
            else
            {
                ChosenAnswers.Remove(assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
            }
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            List<AnswerBool> chosenAnswers = new List<AnswerBool>();
            for (int i = 0; i < assignmentToSolve.AnswerOptions.Count; i++)
            {
                chosenAnswers.Add( new AnswerBool() {Value = false});
            }
            foreach (var ca in ChosenAnswers)
            {
                chosenAnswers[assignmentToSolve.AnswerOptions.IndexOf(ca)].Value = true;
            }
            await answerSender.SendAnswer(new Answer() { AnsweredAssignmentId = assignmentToSolve.AssignmentId,ChosenAnswers = chosenAnswers });
            await Navigation.PushModalAsync(new MultipleResultPage(new MultipleResultViewModel(chosenAnswers, assignmentToSolve)));
            await Navigation.PopAsync();
        }
        private void OnItemSelected(object sender, EventArgs e)
        {
            answerOptionView.SelectedItem = null;
        }
    }
}
