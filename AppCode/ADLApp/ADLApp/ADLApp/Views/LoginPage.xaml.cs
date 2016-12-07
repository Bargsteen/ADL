using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Views;
using ADLApp.Models;
using ADLApp.ViewModel;
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
        public static event EventHandler OnLogin;
    
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            LoginButton.IsEnabled = false;
            UsernameEntry.Text = "elevaa";
            PasswordEntry.Text = "Abekat123$";
            ILogin loginService = new RequestManager();
            var response = await loginService.Login(new UserLoginModel()
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text
            });
            if(response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null && response.Data.IsAuthenticated)
                {
                    App.LoginResult = response.Data;
                    OnLogin?.Invoke(this, e);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Fejl ved login", "Ingen brugere svarer til indtastede information", "Prøv igen");
                }
            }
            else
            {
                await DisplayAlert("Fejl ved login", "Der er ikke forbindelse, har du internet?", "Prøv igen");
            }
            LoginButton.IsEnabled = true;
        }
    }
}
