#region Libraries

using System.Collections.Generic;
using System.Threading.Tasks;
using ADLApp.Models;

#endregion

namespace ADLApp.ViewModel
{
    internal interface ILocationLoader
    {
        Task<List<Location>> GetLocations(string resource);
    }
}