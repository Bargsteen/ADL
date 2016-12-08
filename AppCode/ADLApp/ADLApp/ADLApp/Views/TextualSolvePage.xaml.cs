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
    public partial class TextualSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        public TextualSolvePage(Assignment assignment)
        {
            InitializeComponent();
			SolveSL.Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
						   new Thickness(20, 00, 20, 10),
						   new Thickness(0));
            BindingContext = assignment;
            _assignmentId = assignment.AssignmentId;
        }

        private int _assignmentId = 0;
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            SendAnswerButton.IsEnabled = false;
            if (AnswerEditor.Text != null)
            {
                string status = await
                answerSender.SendAnswer(new Answer(_assignmentId) {AnswerText = AnswerEditor.Text});
                await Navigation.PushModalAsync(new TextualResultPage());
                await Navigation.PopAsync();
            }
            SendAnswerButton.IsEnabled = true;
        }
    }
}
