using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using RestSharp;
using System.Threading;
using Newtonsoft;
using ADLApp.ViewModel;

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private RestClient rClient;
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            rClient = new RestClient("http://activedifferentiatedlearning.azurewebsites.net/api");
            var request = new RestRequest("/GetAssignment/6", Method.GET);
            request.RequestFormat = DataFormat.Json;
            IRestResponse<Assignment> b = await rClient.ExecuteGetTaskAsync<Assignment>(request);
            SolvePage nextPage = new SolvePage(b.Data);
            nextPage.Title = b.Data.Headline;
            await Navigation.PushAsync(nextPage);
            ScanButton.IsEnabled = true;
        }
    }
} 