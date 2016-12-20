#region Libraries

using ADLApp.ViewModel;
using ADLApp.Views;
using Xamarin.Forms;

#endregion

namespace ADLApp
{
    public partial class App
    {
        public static LoginResult LoginResult;

        public App()
        {
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