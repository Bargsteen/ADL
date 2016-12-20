#region Libraries

using System;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class TextualResultPage
    {
        public TextualResultPage()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}