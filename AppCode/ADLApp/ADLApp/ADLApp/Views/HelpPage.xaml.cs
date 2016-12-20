#region Libraries

using System;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class HelpPage
    {
        public HelpPage()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
        }

        private async void OnGoHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}