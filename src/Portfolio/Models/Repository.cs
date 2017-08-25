using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Portfolio.Models
{
    public class Repository
    {

        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string html_url { get; set; }


        public static List<Repository> GetRepositories(int number)
        {
            var client = new RestClient("https://api.github.com/");
            var request = new RestRequest("search/repositories?q=user:" + EnvironmentVariables.UserName + "&sort=stars&order=desc&page=1&per_page=" + number, Method.GET);
            request.AddHeader("user-agent", EnvironmentVariables.UserAgent);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            List<Repository> repos = JsonConvert.DeserializeObject<List<Repository>>(jsonResponse["items"].ToString());
            return repos;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
