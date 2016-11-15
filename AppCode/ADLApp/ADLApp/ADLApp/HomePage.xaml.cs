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

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        private IScanner qrScanner = new QRScanner();
        private IAssignmentLoader assignmentLoader = new AssignmentLoader("http://activedifferentiatedlearning.azurewebsites.net/api");
        public HomePage()
        {
            InitializeComponent();
            ScanButton.BackgroundColor = this.BackgroundColor;
            ScanButton.BorderColor = this.BackgroundColor;
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            Assignment currentassignment = await assignmentLoader.GetAssignment("/GetAssignment/6");
            SolvePage nextPage = new SolvePage(currentassignment as MultipleChoiceAssignment);
            await Navigation.PushAsync(nextPage);
            ScanButton.IsEnabled = true;
        }
        private async void OnClicked(object sender, EventArgs e)
        {
            MultipleChoiceAssignment mpAssignment = new MultipleChoiceAssignment();
            string s = await qrScanner.ScanAndGetOutputString();
            Assignment currentassignment = await assignmentLoader.GetAssignment("/GetAssignment/" + s);
            SolvePage nextPage = new SolvePage(currentassignment as MultipleChoiceAssignment);
            await Navigation.PushAsync(nextPage);
        }
    }
}