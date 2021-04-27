using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ApiApp
{
    public class OutwardCodeService
    {
        #region Properties
        //Restsharp object which handles communication with the API
        public RestClient Client;
        //RestSharp response object
        public IRestResponse RestResponse { get; set; }
        // A newtonsoft object representing the JSON response
        public JObject ResponseContent;
        //an object model of the response
        public OutwardCodeResponse ResponseObject { get; set; }
        //the postcode used in this API request
        public string OutcodeSelected { get; set; }
        #endregion

        //constructor - create the RestClient object
        public OutwardCodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }

        public async Task MakeRequestAsync(string outcode)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            OutcodeSelected = outcode;

            //define request resource path, changing to lowercase and removing whitespace
            request.Resource = $"outcodes/{outcode.ToLower().Replace(" ", "")}";

            //make request
            RestResponse = await Client.ExecuteAsync(request);

            // parse json into a JObject
            ResponseContent = JObject.Parse(RestResponse.Content);

            //parse response body into an object tree
            ResponseObject = JsonConvert.DeserializeObject<OutwardCodeResponse>(RestResponse.Content);
        }
    }
}
