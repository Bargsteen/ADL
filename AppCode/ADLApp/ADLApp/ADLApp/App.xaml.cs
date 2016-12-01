using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADLApp.Views;
using Xamarin.Forms;
using ADLApp.Models;
using ADLApp.ViewModel;

namespace ADLApp
{
    public partial class App : Application
    {
        public static LoginResult LoginResult;
        public App()
        {
            ADLApp.Views.CustomControls.CustomControls.Init();
            InitializeComponent();
            MainPage = new MenuPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
