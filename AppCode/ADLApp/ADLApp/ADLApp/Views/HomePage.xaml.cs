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
using ZXing.Mobile;
using ZXing;
using ADLApp.Models;

namespace ADLApp.Views
{
    public partial class HomePage : ContentPage
    {
        private IScanner qrScanner = new QRScanner();
        private IAssignmentLoader assignmentLoader = new RequestManager("http://activedifferentiatedlearning.azurewebsites.net/api");
        public HomePage()
        {
            InitializeComponent();
            ScanButton.BackgroundColor = BackgroundColor;
            ScanButton.BorderColor = BackgroundColor;
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            string s = await qrScanner.ScanAndGetOutputString();
            if (s != "")
            {
                Assignment currentassignment = await assignmentLoader.GetAssignment("/GetAssignment/" + s);
                if (currentassignment is MultipleChoiceAssignment)
                {
                    SolvePage nextPage = new SolvePage(currentassignment as MultipleChoiceAssignment);
                    await Navigation.PushAsync(nextPage);
                }
                else
                {
                    SolvePage nextPage = new SolvePage(currentassignment);
                    await Navigation.PushAsync(nextPage);
                }
            }
            ScanButton.IsEnabled = true;
        }
        private async void OnClicked(object sender, EventArgs e)
        {
        }
    }
}