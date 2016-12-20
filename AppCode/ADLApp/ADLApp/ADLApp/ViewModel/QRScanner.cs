#region Libraries

using System.Threading.Tasks;
using ZXing;
using ZXing.Mobile;

#endregion

namespace ADLApp.ViewModel
{
    internal class QrScanner : IScanner
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
            return "cancel";
        }
    }
}