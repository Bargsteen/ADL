using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADL.Models;
using ADL.Models.Assignments;
using Xamarin.Forms;
using ADLApp.ViewModel;

namespace ADLApp.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IScanner _qrScanner = new QrScanner();
        private readonly IAssignmentLoader _assignmentLoader = new RequestManager();
        private readonly ILocationLoader _locationLoader = new RequestManager();

        private List<Location> _locations;
        public HomePage()
        {
            InitializeComponent();
            PromptForLogin();
            LoginPage.OnLogin += OnLoginLoadLocations;
        }

        private async void OnLoginLoadLocations(object sender, EventArgs e)
        {
            _locations = await _locationLoader.GetLocations("Locationlist");
        }

        private async void PromptForLogin()
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            string scanString = await _qrScanner.ScanAndGetString();
            if (scanString == "cancel")
            {
                ScanButton.IsEnabled = true;
                return;
            }
            if (scanString != "error")
            {
                Assignment currentAssignment = await _assignmentLoader
                .GetAssignment(scanString);
                if (currentAssignment != null)
                {
                    if (currentAssignment.Type == EnumCollection.AssignmentType.ExclusiveChoice)
                    {
                        ExclusiveSolvePage nextPage = new ExclusiveSolvePage(currentAssignment);
                        await Navigation.PushAsync(nextPage);
                    }
                    else if (currentAssignment.Type == EnumCollection.AssignmentType.MultipleChoice)
                    {
                        MultipleSolvePage nextPage = new MultipleSolvePage(currentAssignment);
                        await Navigation.PushAsync(nextPage);
                    }
                    else
                    {
                        TextualSolvePage nextPage = new TextualSolvePage(currentAssignment);
                        await Navigation.PushAsync(nextPage);
                    }
                }
                else
                {
                    await DisplayAlert("Kode er ikke koblet på opgave", "Koden har ikke en opgave", "Prøv igen");
                }
            }
            else
            {
                await DisplayAlert("Fejl ved scanning af opgaver", "Det er ikke en adl qr kode", "Prøv med en anden");
            }
            ScanButton.IsEnabled = true;
        }
        private async void OnFindButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FindQrPage(_locations));
        }
    }
}