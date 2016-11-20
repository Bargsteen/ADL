using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models;
using System.Net;

namespace ADLApp.ViewModel
{
    interface IAnswerSender
    {
        Task<HttpStatusCode> SendAnswer(Answer answer);
    }
}
