using System;
using ADLApp.Models;
using ADLApp.ViewModel;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class ExclusiveResultPage : ContentPage
    {
        public ExclusiveResultPage(ExclusiveResultViewModel RVM)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(0, 20, 0, 0),
                            new Thickness(0),
                            new Thickness(0));
            BindingContext = RVM;
        }
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
