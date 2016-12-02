
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

        {
            IsPresented = false;
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}
