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
        public async Task<string> ScanAndGetString()
        {
            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            Result res = await scanner?.Scan();
            if (res != null)
            {
                if (res.Text.Contains("ADLearning"))
                {
                    //removes "ADLearning" from "ADLearning;300" for example
                    string[] subStrings = res.Text.Split(';');
                    return subStrings[1];
                }
                return "error";
            }
            return null;
        }
    }
}
