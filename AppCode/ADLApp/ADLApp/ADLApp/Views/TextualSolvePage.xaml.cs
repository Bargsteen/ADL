using ADLApp.Models;
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
        public TextualSolvePage(Assignment assignment)
        {
            InitializeComponent();
            BindingContext = assignment;
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if(AnswerEditor.Text != null)
            {

            }
        }
    }
}
