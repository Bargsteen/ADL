using ADLApp.Models;
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
        public MultipleSolvePage(MultipleChoiceAssignment mca)
        {
            new CheckBox();
            InitializeComponent();
            BindingContext = mca;
        }
        private async void OnChecked(object sender, bool isChecked)
        {
        }
        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            return;
        }
    }
}
