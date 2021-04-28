using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.PostcodesIOService
{
    public class OutwardCodeDTO
    {
        public OutwardCodeResponse OutwardCodeResponse { get; set; }
        public void DeserializeResponse(string postcodeResponse)
        {
            OutwardCodeResponse = JsonConvert.DeserializeObject<OutwardCodeResponse>(postcodeResponse);
        }
    }
}
