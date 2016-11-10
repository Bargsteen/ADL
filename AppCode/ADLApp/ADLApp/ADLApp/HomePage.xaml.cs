using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using RestSharp;

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            var client = new RestClient("http://www.localhost:5000/api/GetAssignment/");
            var request = new RestRequest("1", Method.GET);
            client.ExecuteAsync(request, response =>
            {
                ScanButton.Text = response.Content;
            });

            //await Navigation.PushAsync(new SolvePage());
        }
    }
}
