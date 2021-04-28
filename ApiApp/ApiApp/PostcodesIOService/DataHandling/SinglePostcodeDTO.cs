using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.PostcodesIOService
{
    public class SinglePostcodeDTO
    {
        public SinglePostcodeResponse SinglePostcodeResponse { get; set; }
        public void DeserializeResponse(string postcodeResponse)
        {
            SinglePostcodeResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(postcodeResponse);
        }
    }
}
