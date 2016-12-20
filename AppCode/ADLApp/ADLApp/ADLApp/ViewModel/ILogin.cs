#region Libraries

using System.Threading.Tasks;
using RestSharp;

#endregion

namespace ADLApp.ViewModel
{
    public interface ILogin
    {
        Task<IRestResponse<LoginResult>> Login(UserLoginModel userinfo);
    }
}