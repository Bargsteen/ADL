#region Libraries

using System;
using ADLApp.Models;
using ADLApp.ViewModel;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class TextualSolvePage
    {
        private readonly int _assignmentId;
        private readonly IAnswerSender _answerSender = new RequestManager();

        public TextualSolvePage(Assignment assignment)
        {
            InitializeComponent();
            SolveSl.Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
            BindingContext = assignment;
            _assignmentId = assignment.AssignmentId;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            SendAnswerButton.IsEnabled = false;
            if (AnswerEditor.Text != null)
            {
                string status = await
                    _answerSender.SendAnswer(new Answer(_assignmentId) {AnswerText = AnswerEditor.Text});
                await Navigation.PushModalAsync(new TextualResultPage());
                await Navigation.PopAsync();
            }
            SendAnswerButton.IsEnabled = true;
        }
    }
}