using System;
using ADLApp.ViewModel;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class ExclusiveResultPage : ContentPage
    {
        public ExclusiveResultPage(ExclusiveResultViewModel RVM)
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
