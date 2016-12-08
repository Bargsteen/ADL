using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ADL.Models.Answers;

namespace ADLApp.ViewModel
{
    interface IAnswerSender
    {
        Task<string> SendAnswer(Answer answer);
    }
}
