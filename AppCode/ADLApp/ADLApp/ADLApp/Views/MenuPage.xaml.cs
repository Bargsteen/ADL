#region Libraries

using System;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class MenuPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            IsPresented = false;
            await Navigation.PushModalAsync(new LoginPage());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            IsPresented = false;
            await Navigation.PushModalAsync(new HelpPage());
        }
    }
}