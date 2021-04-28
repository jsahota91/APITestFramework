using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ApiApp.PostcodesIOService
{
    public class OutwardCodeService
    {
        #region Properties
        public CallManager CallManager { get; }
        // A newtonsoft object representing the JSON response
        public JObject ResponseContent;
        //the postcode used in this API request
        //an object model of the response
        public OutwardCodeDTO OutwardCodeDTO { get; set; }
        //the postcode used in this API Request
        public string OutwardCodeSelected { get; set; }
        public string OutwardCodeResponse { get; set; }


        #endregion

        //constructor - create the RestClient object
        public OutwardCodeService()
        {
            CallManager = new CallManager();
            OutwardCodeDTO = new OutwardCodeDTO();
        }

        public async Task MakeRequestAsync(string outcode)
        {
            //make request
            OutwardCodeResponse = await CallManager.MakeOutwardCodeRequest(outcode);

            // parse json into a JObject
            ResponseContent = JObject.Parse(OutwardCodeResponse);

            //parse response body into an object tree
            OutwardCodeDTO.DeserializeResponse(OutwardCodeResponse);
        }
    }
}
