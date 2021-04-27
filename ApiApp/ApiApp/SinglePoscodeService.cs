using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ApiApp
{
    public class SinglePostcodeService
    {
        #region Properties
        //Restsharp object which handles communication with the API
        public RestClient Client;
        //RestSharp response object
        public IRestResponse RestResponse { get; set; }
        // A newtonsoft object representing the JSON response
        public JObject ResponseContent;
        //the postcode used in this API request
        //an object model of the response
        public SinglePostcodeResponse ResponseObject { get; set; }
        public string PostcodeSelected { get; set; }
        #endregion

        //constructor - create the RestClient object
        public SinglePostcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }

        public async Task MakeRequestAsync(string postcode)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            PostcodeSelected = postcode;

            //define request resource path, changing to lowercase and removing whitespace
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            //make request
            RestResponse = await Client.ExecuteAsync(request);

            // parse json into a JObject
            ResponseContent = JObject.Parse(RestResponse.Content);

            //parse response body into an object tree
            ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(RestResponse.Content);
        }
    }
}
