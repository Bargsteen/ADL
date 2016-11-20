using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing;

namespace ADLApp.ViewModel
{
    class QRScanner : IScanner
    {
        public async Task<string> ScanAndGetOutputString()
        {
            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            string lol = "";
            Result res = await scanner?.Scan();
            if (res != null) return res.Text;
            else return "";
        }
    }
}
