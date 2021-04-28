using System;
using System.Threading.Tasks;
using RestSharp;

namespace ApiApp.PostcodesIOService
{
    public class CallManager
    {
        //Restsharp object which handles communication with the API
        readonly IRestClient _client;

        public IRestResponse Response { get; set; }

        public CallManager()
        {
            _client = new RestClient(AppConfigReader.BaseUrl);
        }

/// <summary>
/// defines and makes the API request, and stores the response
/// </summary>
/// <param name="postcode"></param>
/// <returns>The response content</returns>
/// 
        public async Task<string> MakeSinglePostcodeRequest(string postcode)
        {
            //set up request
            var request = new RestRequest();
            request.AddParameter("Content-Type", "application/json");

            //define request resource path, changing to lowercase and removing whitespace
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            //make request
            var response = await _client.ExecuteAsync(request);
            return response.Content;
            
        }

        public async Task<string> MakeOutwardCodeRequest(string outcode)
        {
            //set up request
            var request = new RestRequest();
            request.AddParameter("Content-Type", "application/json");

            //define request resource path, changing to lowercase and removing whitespace
            request.Resource = $"outcodes/{outcode.ToLower().Replace(" ", "")}";

            //make request
            Response = await _client.ExecuteAsync(request);
            return Response.Content;

        }
    }
}
