using ADL.Models.Answers;
using ADLApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADL.Models.Assignments;
using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class TextualSolvePage : ContentPage
    {
        private IAnswerSender answerSender = new RequestManager();
        public TextualSolvePage(Assignment assignment)
        {
            InitializeComponent();
            BindingContext = assignment;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (AnswerEditor.Text != null)
            {
                string status = await
                answerSender.SendAnswer(new Answer() {AnsweredAssignmentId = (BindingContext as Assignment).AssignmentId, AnswerText = AnswerEditor.Text});
                await Navigation.PushModalAsync(new TextualResultPage());
                await Navigation.PopAsync();
            }
        }
    }
}
