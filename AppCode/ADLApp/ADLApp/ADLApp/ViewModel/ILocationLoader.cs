using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models;


namespace ADLApp.ViewModel
{
    interface ILocationLoader
    {
        Task<List<Location>> GetLocations(string resource);
    }
}
