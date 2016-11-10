using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Threading.Tasks;

namespace RestTEst
{
    class Program
    {
        public static int Counter = 3;
        static void Main(string[] args)
        {
            for (int Counter = 3; Counter < 6; Counter++)
            {
                var rClient = new RestClient("http://localhost:5000/api/");
                var request = new RestRequest($"GetAssignment/{Counter}", Method.GET);
                var response = rClient.Execute<Item>(request);
                Console.WriteLine(response.Data.assignmentID);
                Console.WriteLine(response.Data.headline);
                Console.WriteLine();
                Console.WriteLine(response.Data.question);
                Console.WriteLine(response.Data.answerOptionOne);
                Console.WriteLine(response.Data.answerOptionTwo);
            }
            //var client = new RestClient("http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/");
            //var request = new RestRequest("198440/allinfo", Method.GET);
            //var response = client.Execute<Item>(request);
            //Console.WriteLine(response.Data.rxtermsProperties);

            //rClient.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.ErrorMessage);
            //    Console.WriteLine(response.StatusCode);
            //    Console.WriteLine(response.StatusDescription);
            //    Console.WriteLine(response.ResponseStatus);
            //    pik = response.Content;
            //    Console.ReadLine();
            //});
            Console.ReadKey();
        }
        public class Item
        {
            public int assignmentID { get; set; }
            public string headline { get; set; }
            public string question { get; set; }
            public string answerOptionOne { get; set; }
            public string answerOptionTwo { get; set; }
            public string answerOptionThree { get; set; }
            public string answerOptionFour { get; set; }
            public Item()
            {

            }
        }
    }
}
