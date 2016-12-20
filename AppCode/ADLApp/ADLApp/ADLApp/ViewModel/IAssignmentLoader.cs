#region Libraries

using System.Threading.Tasks;
using ADLApp.Models;

#endregion

namespace ADLApp.ViewModel
{
    internal interface IAssignmentLoader
    {
        Task<Assignment> GetAssignment(string resource);
    }
}