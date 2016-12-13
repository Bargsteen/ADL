using System;
using System.Text;
using System.Collections.Generic;
using ADLApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace ADLApp.UnitTest
{
    /// <summary>
    /// Summary description for LoginTests
    /// </summary>
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        [TestCategory("Login")]
        public async void TestLogin()
        {
            ILogin loginService = new RequestManager();
            IRestResponse<LoginResult> resp = await loginService.Login(new UserLoginModel() { Password = "eleva", Username = "Abekat123$" });
            Assert.IsNotNull(resp.Data.UserId);
        }
    }
}
