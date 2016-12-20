#region Libraries

using System.Collections.Generic;
using ADLApp.Models;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class FindPage
    {
        public FindPage(List<Location> locations)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 10),
                new Thickness(10, 00, 10, 10),
                new Thickness(0));
            BindingContext = locations;
        }
    }
}