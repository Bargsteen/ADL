using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models;
using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class FindQrPage : ContentPage
    {
        public FindQrPage(List<Location> locations )
        {
            
            InitializeComponent();
			Padding = Device.OnPlatform(new Thickness(20, 20, 20, 0),
						   new Thickness(10, 00, 10, 00),
						   new Thickness(0));
            this.BindingContext = locations;
        }
    }
}
