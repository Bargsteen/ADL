using ADLApp.Models;
using ADLApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class MultipleSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        List<AnswerOption> ChosenAnswers = new List<AnswerOption>();
        public MultipleSolvePage(MultipleChoiceAssignment mca)
        {
            InitializeComponent();
            BindingContext = mca;
            assignmentToSolve = mca;
        }
        private MultipleChoiceAssignment assignmentToSolve;
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
            List<string> answerIndices = new List<string>();
            foreach (AnswerOption ao in ChosenAnswers)
            {
                answerIndices.Add(assignmentToSolve.AnswerOptions.IndexOf(ao).ToString());
            }

            await answerSender.SendAnswer(new Answer(assignmentToSolve.AssignmentId, answerIndices.ToArray()));
            await Navigation.PushModalAsync(new MultipleResultPage(new MultipleResultViewModel(ChosenAnswers, assignmentToSolve)));
            await Navigation.PopAsync();
        }
        private void OnItemSelected(object sender, EventArgs e)
        {
            answerOptionView.SelectedItem = null;
        }
    }
}
