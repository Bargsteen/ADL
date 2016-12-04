using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADLApp.ViewModel;
using ADLApp.Models;

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
                currentAssignment = new MultipleChoiceAssignment()
                {
                    Question = "Hvad hedder Teitur?",
                    Headline = "Om teitur",
                    AnswerCorrectness = new List<bool>() { true, false, true },
                    AnswerOptions = new List<AnswerOption>() { new AnswerOption() {
                        AnswerOptionID = 1, Text = "mig"
                    }, new AnswerOption() {
                        AnswerOptionID = 2, Text = "eller mig"
                    }, new AnswerOption() {
                        AnswerOptionID = 3, Text = "måske mig?"
                    }
                    }
                };
                if (currentAssignment != null)
                {
                    if (currentAssignment is ExclusiveChoiceAssignment)
                    {
                        ExclusiveSolvePage nextPage = new ExclusiveSolvePage(currentAssignment as ExclusiveChoiceAssignment);
                        await Navigation.PushAsync(nextPage);
                    }
                    else if (currentAssignment is MultipleChoiceAssignment)
                    {
                        MultipleSolvePage nextPage = new MultipleSolvePage(currentAssignment as MultipleChoiceAssignment);
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