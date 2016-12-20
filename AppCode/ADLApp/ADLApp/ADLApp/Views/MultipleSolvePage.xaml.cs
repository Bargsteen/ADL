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
        private readonly IAnswerSender _answerSender = new RequestManager();
        private readonly Assignment _assignmentToSolve;
        private readonly List<AnswerOption> _chosenAnswers = new List<AnswerOption>();

        public MultipleSolvePage(Assignment mca)
        {
            InitializeComponent();
            TextSl.Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
            BindingContext = mca;
            _assignmentToSolve = mca;
        }

        private void OnChecked(object sender, bool isChecked)
        {
            if (isChecked)
                _chosenAnswers.Add(
                    _assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
            else
                _chosenAnswers.Remove(
                    _assignmentToSolve.AnswerOptions.First(ao => ao.Text == (sender as CheckBox).Text));
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            SendAnswerButton.IsEnabled = false;
            List<AnswerBool> chosenAnswers = new List<AnswerBool>();
            for (int i = 0; i < _assignmentToSolve.AnswerOptions.Count; i++)
                chosenAnswers.Add(new AnswerBool {Value = false});
            foreach (var ca in _chosenAnswers)
                chosenAnswers[_assignmentToSolve.AnswerOptions.IndexOf(ca)].Value = true;
            await
                _answerSender.SendAnswer(new Answer(_assignmentToSolve.AssignmentId)
                {
                    ChosenAnswers = chosenAnswers
                });
            await
                Navigation.PushModalAsync(
                    new MultipleResultPage(new MultipleResultViewModel(chosenAnswers, _assignmentToSolve)));
            await Navigation.PopAsync();
            SendAnswerButton.IsEnabled = true;
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            AnswerOptionView.SelectedItem = null;
        }
    }
}