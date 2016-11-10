using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using ADLApp.Model;
using RestSharp;

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private RestClient rClient;
        private Uri rURL;
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            rClient = new RestClient("localhost:5000/api/");
            var request = new RestRequest("/GetAssignment/1");
            var response = rClient.Execute<Item>(request);
            ScanButton.Text = response.Data.headline;
            //await Navigation.PushAsync(new SolvePage());
        }
    }
}
