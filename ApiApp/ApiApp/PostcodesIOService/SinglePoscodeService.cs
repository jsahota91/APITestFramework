using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace ApiApp.PostcodesIOService
{
    public class SinglePostcodeService
    {
        #region Properties
        public CallManager CallManager { get;  }
        // A newtonsoft object representing the JSON response
        public JObject ResponseContent;
        //the postcode used in this API request
        //an object model of the response
        public SinglePostcodeDTO SinglePostcodeDTO { get; set; }
        //the postcode used in this API Request
        public string PostcodeSelected { get; set; }
        public string PostcodeResponse { get; set; }
        
        #endregion

        //constructor - create the RestClient object
        public SinglePostcodeService()
        {
            CallManager = new CallManager();
            SinglePostcodeDTO = new SinglePostcodeDTO();
        }

        public async Task MakeRequestAsync(string postcode)
        {
            PostcodeSelected = postcode;
            //make request
            PostcodeResponse = await CallManager.MakeSinglePostcodeRequest(postcode);

            // parse json into a JObject
            ResponseContent = JObject.Parse(PostcodeResponse);

            //parse response body into an object tree
            SinglePostcodeDTO.DeserializeResponse(PostcodeResponse);
        }

        public int CodesCount()
        {
            return ResponseContent["result"]["codes"].Count();
        }
    }
}
