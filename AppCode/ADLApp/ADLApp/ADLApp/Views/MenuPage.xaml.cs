
using System;
using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
			Padding = Device.OnPlatform(new Thickness(20, 20, 20, 0),
						   new Thickness(10, 00, 10, 00),
						   new Thickness(0));
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
