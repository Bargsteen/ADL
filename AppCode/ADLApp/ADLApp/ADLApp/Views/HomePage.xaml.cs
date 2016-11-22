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
            string scanString = await qrScanner.ScanAndGetOutputString();
            if (scanString != "")
            {
                string[] strings = scanString.Split(';');
                Assignment currentassignment = await assignmentLoader
                    .GetAssignment("/location/" + strings[1]);
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
                    await DisplayAlert("Fejl ved indlæsning af opgave", "Er det en adl qr kode?", "Prøv igen");
                }
            }
            else if (scanString == "error")
            {
                await DisplayAlert("Fejl ved scanning af opgaver", "Det er ikke en adl qr kode", "Prøv med en anden");
            }
            ScanButton.IsEnabled = true;
        }
    }
}