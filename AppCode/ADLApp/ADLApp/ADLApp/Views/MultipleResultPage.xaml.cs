#region Libraries

using System;
using ADLApp.ViewModel;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class MultipleResultPage
    {
        public MultipleResultPage(MultipleResultViewModel mrvm)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
            BindingContext = mrvm;
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            answerOptionView.SelectedItem = null;
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}