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
            Result res = await GetResult();
            return res.Text;
        }
        public async Task<Result> GetResult()
        {
            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            return await scanner.Scan();
        }
    }
}
