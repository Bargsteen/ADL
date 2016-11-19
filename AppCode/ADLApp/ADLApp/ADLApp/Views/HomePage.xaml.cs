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
        private IAssignmentLoader assignmentLoader = new AssignmentLoader("http://activedifferentiatedlearning.azurewebsites.net/api");
        public HomePage()
        {
            InitializeComponent();
            ScanButton.BackgroundColor = BackgroundColor;
            ScanButton.BorderColor = BackgroundColor;
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            MultipleChoiceAssignment mpAssignment = new MultipleChoiceAssignment();
            await Navigation.PushAsync(new SolvePage(mpAssignment));
            string s = await qrScanner.ScanAndGetOutputString();
            if (s != "")
            {
                Assignment currentassignment = await assignmentLoader.GetAssignment("/GetAssignment/" + s);
                SolvePage nextPage = new SolvePage(currentassignment as MultipleChoiceAssignment);
                await Navigation.PushAsync(nextPage);
            }
            ScanButton.IsEnabled = true;
        }
        private async void OnClicked(object sender, EventArgs e)
        {
        }
    }
}