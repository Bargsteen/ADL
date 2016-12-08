using ADLApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.ViewModel
{
    interface IAssignmentLoader
    {
        Task<Assignment> GetAssignment(string resource);
    }
}
