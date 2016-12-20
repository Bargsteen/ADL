#region Libraries

using System;
using ADLApp.ViewModel;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class ExclusiveResultPage
    {
        public ExclusiveResultPage(ExclusiveResultViewModel RVM)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(20, 00, 20, 10),
                new Thickness(0));
            BindingContext = RVM;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}