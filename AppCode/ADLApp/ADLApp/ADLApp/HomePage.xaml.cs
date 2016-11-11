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
using ADLApp.Models;

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
            rClient = new RestClient("http://activedifferentiatedlearning.azurewebsites.net/api");
            var request = new RestRequest("/GetAssignment/5", Method.GET);
            request.RequestFormat = DataFormat.Json;
            IRestResponse<Assignment> b = await rClient.ExecuteGetTaskAsync<Assignment>(request);
            await Navigation.PushAsync(new SolvePage(b.Data));
        }
    }
}
