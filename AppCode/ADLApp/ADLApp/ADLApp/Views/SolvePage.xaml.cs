using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADLApp.ViewModel;
using System.Collections.ObjectModel;
using XLabs.Forms.Controls;
using Xamarin.Forms.Pages;
using ADLApp.Models;

namespace ADLApp.Views
{
    public partial class SolvePage : ContentPage
    {
        public string[] answerOptions { get; set; }
        public MultipleChoiceAssignment lolAssignment { get; set; }
        public SolvePage(Assignment currentAssignment)
        {
            InitializeComponent();
            BindingContext = lolAssignment;
            lolAssignment = currentAssignment as MultipleChoiceAssignment;
            Title = lolAssignment.Headline;
            lolAssignment.LoadData();
            Question.Text = this.lolAssignment.Question;
            answerOptionView.ItemsSource = lolAssignment.AnswerOptions.Values;
        }

        private async void OnSendAnswerButtonClicked(object sender, EventArgs e)
        {
            if (lolAssignment is MultipleChoiceAssignment)
            {
                if (Array.IndexOf(lolAssignment.AnswerOptions.Values.ToArray(), answerOptionView.SelectedItem) == ((MultipleChoiceAssignment)lolAssignment).CorrectAnswer)
                {
                    this.BackgroundColor = Color.Green;
                }
                else
                {
                    this.BackgroundColor = Color.Red;
                }
            }
        }
    }
}
