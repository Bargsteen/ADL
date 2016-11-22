using System;
using ADLApp.Models;
using ADLApp.ViewModel;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage(ResultViewModel RVM)
        {
            InitializeComponent();
            BindingContext = RVM;
        }
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
