﻿#region Libraries

using System.Collections.Generic;
using System.Threading.Tasks;
using ADLApp.Models;
using Newtonsoft.Json;
using RestSharp;

#endregion

namespace ADLApp.ViewModel
{
    public class RequestManager : IAssignmentLoader, IAnswerSender, ILocationLoader, ILogin
    {
        private readonly IRestClient _rClient = new RestClient("http://adlearning.azurewebsites.net/api");

        public async Task<string> SendAnswer(Answer answer)
        {
            answer.UserId = App.LoginResult.UserId;
            RestRequest request = new RestRequest($"/SendAnswer", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(answer);
            IRestResponse resp = await _rClient.ExecutePostTaskAsync(request);
            return resp.Content;
        }

        /// <summary>
        ///     Gets assignment based from AssigmentLoader, with the initialized client.
        ///     Needs a Method from the controller with right input.
        /// </summary>
        /// <param name="resourceLocationId"></param>
        /// <returns></returns>
        public async Task<Assignment> GetAssignment(string resourceLocationId)
        {
            TaskFactory tf = new TaskFactory();
            RestRequest request =
                new RestRequest("/location/" + resourceLocationId + "?personId=" + App.LoginResult.UserId,
                    Method.GET);
            IRestResponse response = await GetDataAsString(request);
            if (response.Content != "Lokationen har ikke nogen opgave")
                return JsonConvert.DeserializeObject<Assignment>(response.Content);
            return null;
        }

        public async Task<List<Location>> GetLocations(string resource)
        {
            RestRequest request = new RestRequest(resource + "?personId=" + App.LoginResult.UserId, Method.GET);
            request.RequestFormat = DataFormat.Json;
            IRestResponse<List<Location>> response =
                await _rClient.ExecuteGetTaskAsync<List<Location>>(request);
            return response.Data;
        }

        public async Task<IRestResponse<LoginResult>> Login(UserLoginModel userinfo)
        {
            RestRequest request = new RestRequest("GetIdentity", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(userinfo);
            IRestResponse<LoginResult> resp = await _rClient.ExecutePostTaskAsync<LoginResult>(request);
            return resp;
        }

        private async Task<IRestResponse> GetDataAsString(RestRequest request)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddBody(App.LoginResult.UserId);
            IRestResponse response = await _rClient.ExecuteGetTaskAsync(request);
            return response;
        }
    }
}