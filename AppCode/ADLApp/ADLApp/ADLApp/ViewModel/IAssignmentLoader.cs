using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADL.Models.Assignments;

namespace ADLApp.ViewModel
{
    interface IAssignmentLoader
    {
        Task<Assignment> GetAssignment(string resource);
    }
}
