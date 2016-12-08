
using System;
using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class MenuPage : MasterDetailPage
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
