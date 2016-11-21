using System;
using Xamarin.Forms;
using ADLApp.ViewModel;
using ADLApp.Models;

namespace ADLApp.Views
{
    public partial class HomePage : ContentPage
    {
        private IScanner qrScanner = new QRScanner();
        private IAssignmentLoader assignmentLoader = new RequestManager("http://adlearning.azurewebsites.net/api");
        public HomePage()
        {
            InitializeComponent();
            ScanButton.BackgroundColor = BackgroundColor;
            ScanButton.BorderColor = BackgroundColor;
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
           // ScanButton.Text = "Scan qr kode";
            ScanButton.IsEnabled = false;
            string s = await qrScanner.ScanAndGetOutputString();
            if (s != "")
            {
                Assignment currentassignment = await assignmentLoader.GetAssignment("/location/" + s);
                if (currentassignment != null)
                {
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
                else
                {
                    await DisplayAlert("Fejl ved indlæsning af opgave", "Er det en ADL qr kode?", "OK");
                  //  ScanButton.Text += ".. Er det en ADL qr kode?";
                }
            }
            ScanButton.IsEnabled = true;
        }
        private async void OnClicked(object sender, EventArgs e)
        {
        }
    }
}