using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ApiApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Get Request
            var restClient = new RestClient("https://api.postcodes.io/");
            var restRequest = new RestRequest();
            restRequest.Method = Method.GET;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.Timeout = -1;

            var postcode = "EC2Y 5AS";
            restRequest.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            var restResponse = await restClient.ExecuteAsync(restRequest);
            Console.WriteLine("Response content (string):");
            Console.WriteLine(restResponse.Content);

            var jsonResponse = JObject.Parse(restResponse.Content);
            Console.WriteLine("\nResponse content as a JObject");
            Console.WriteLine(jsonResponse);

            //Print out different values, such as the admin district, admin ward or the parish code.
            var adminDistrict = jsonResponse["result"]["admin_district"];
            Console.WriteLine($"Admin district: {adminDistrict}");

            var adminWard = jsonResponse["result"]["codes"]["admin_ward"];
            Console.WriteLine($"Admin Ward: {adminWard}");

            var parish = jsonResponse["result"]["codes"]["parish"];
            Console.WriteLine($"Parish: {parish}");


            var singlePostcodeResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(restResponse.Content);
            //var client = new RestClient("https://api.postcodes.io/postcodes/EC2Y5AS");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cookie", "__cfduid=d1f86993c4cf67facefb3a6ee0faeefd31619433740");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //POST Request
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=d1f86993c4cf67facefb3a6ee0faeefd31619433740");
            request.AddParameter("application/json", "{\r\n    \"postcodes\" : [\"OX49 5NU\", \"M32 OJG\", \"NE30 1DP\"]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            //convert response from bulk POST request above to a JObject.
            var jsonPostResponse = JObject.Parse(response.Content);
            Console.WriteLine("\nResponse Post content as a JObject");
            Console.WriteLine(jsonPostResponse);

            //Try accessing the admin district of the second postcode, or the first postcode's nuts code
            var adminDistrict2 = jsonPostResponse["result"][2]["result"]["nuts"];
            Console.WriteLine($"second postcode of Admin district: {adminDistrict2}");

            var bulkJsonResponse = JObject.Parse(response.Content);
            var bulkPostcodeResponseObject = JsonConvert.DeserializeObject<BulkPostcodeResponse>(response.Content);
            var adminDistrictFromBPR = bulkPostcodeResponseObject.result[1].result.admin_district;
            var quality = bulkPostcodeResponseObject.result[0].result.quality;
        }
    }
}
