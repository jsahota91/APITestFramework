using NUnit.Framework;
using System.Threading.Tasks;
using ApiApp.PostcodesIOService;

namespace APITests.Tests
{
    //happy paths in this class
    public class WhenTheSinglePostcodeServiceIsCalled_WithValidPostcode
    {
        SinglePostcodeService _singlePostcodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _singlePostcodeService = new SinglePostcodeService();
            await _singlePostcodeService.MakeRequestAsync("EC2Y 5AS");
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_singlePostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            Assert.That(_singlePostcodeService.SinglePostcodeDTO.SinglePostcodeResponse.status, Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _singlePostcodeService.ResponseContent["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void AdminDistrict_IsCityOfLondon()
        {
            Assert.That(_singlePostcodeService.SinglePostcodeDTO.SinglePostcodeResponse.result.admin_district, Is.EqualTo("City of London"));
        }

        [Test]
        public void NumberOfCodes_IsCorrect()
        {
            Assert.That(_singlePostcodeService.CodesCount(), Is.EqualTo(12));
        }
    }
}
