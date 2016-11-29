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
            this.BindingContext = locations;
        }
    }
}
