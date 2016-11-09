using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            var FindQRKode = new Image { Aspect = Aspect.AspectFit };
            FindQRKode.Source = ImageSource.FromFile("find_qr.png");
            var ScanQRKode = new Image { Aspect = Aspect.AspectFit };
            FindQRKode.Source = ImageSource.FromFile("scan_qr.png");
        }
        public void OnScanButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
