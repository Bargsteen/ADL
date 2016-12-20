#region Libraries

using System.Threading.Tasks;

#endregion

namespace ADLApp.ViewModel
{
    internal interface IScanner
    {
        Task<string> ScanAndGetString();
    }
}