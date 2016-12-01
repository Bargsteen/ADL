using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.ViewModel
{
    interface ILogin
    {
        Task<IRestResponse<LoginResult>> Login(UserLoginModel userinfo);       
    }
}
