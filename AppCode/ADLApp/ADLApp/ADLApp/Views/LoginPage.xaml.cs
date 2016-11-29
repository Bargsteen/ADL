using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Views;
using ADLApp.Models;
using ADLApp.ViewModel;
using System.Security;
using Xamarin.Forms;
using System.Net;

namespace ADLApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            ILogin loginService = new RequestManager();
            var response = await loginService.Login(new LoginModel()
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text
            });
            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {
                await DisplayAlert("Fejl ved login", "Der er ikke nogle brugere med denne information", "Prøv igen");
            }
            //await Navigation.PopModalAsync();
        }
    }
}
