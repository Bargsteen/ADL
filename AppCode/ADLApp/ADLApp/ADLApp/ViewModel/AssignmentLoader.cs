﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using RestSharp;
using Newtonsoft.Json;

namespace ADLApp.ViewModel
{
    class AssignmentLoader : IAssignmentLoader
    {
        private RestClient rClient;
        /// <summary>
        /// Needs a client aka url of the API controller
        /// </summary>
        /// <param name="baseURL"></param>
        public AssignmentLoader(string baseURL)
        {
            rClient = new RestClient(baseURL);
        }
        /// <summary>
        /// Gets assignment based from AssigmentLoader, with the initialized client.
        /// Needs a Method from the controller with right input.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public async Task<Assignment> GetAssignment(string resource)
        {
            return await GetAssignmentString(resource);
        }
        public async Task<Assignment> GetAssignmentString(string resource)
        {
            RestRequest request = new RestRequest(resource, Method.GET);
            IRestResponse response = await GetDataAsString(request);
            //Check object it has to create. Switch on a data in the json format("assignmentType":"MultipleChoice" for example
            return JsonConvert.DeserializeObject<MultipleChoiceAssignment>(response.Content);
        }
        private async Task<IRestResponse> GetDataAsString(RestRequest request)
        {
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = await rClient.ExecuteGetTaskAsync(request);
            return response;
        }
    }
}