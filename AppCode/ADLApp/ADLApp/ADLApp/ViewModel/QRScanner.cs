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
            return await HandleResult(res);
        }
        public async Task<Result> GetResult()
        {
            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            return await scanner.Scan();
        }
        private async Task<string> HandleResult(ZXing.Result res)
        {
            if (res != null)
            {
                return res.Text.ToString();
            }
            else return ".....";
        }
    }
}
