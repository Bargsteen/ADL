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
            BindingContext = assignment;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (AnswerEditor.Text != null)
            {
                string status = await
                answerSender.SendAnswer(new TextAnswer((BindingContext as Assignment).AssignmentId) {Answer = AnswerEditor.Text});
                await Navigation.PushModalAsync(new TextualResultPage());
                await Navigation.PopAsync();
            }
        }
    }
}
