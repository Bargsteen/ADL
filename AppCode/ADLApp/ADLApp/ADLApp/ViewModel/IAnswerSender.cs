#region Libraries

using System.Threading.Tasks;
using ADLApp.Models;

#endregion

namespace ADLApp.ViewModel
{
    internal interface IAnswerSender
    {
        Task<string> SendAnswer(Answer answer);
    }
}