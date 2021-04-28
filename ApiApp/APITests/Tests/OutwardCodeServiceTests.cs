using NUnit.Framework;
using System.Threading.Tasks;
using ApiApp.PostcodesIOService;

namespace APITests.Tests
{
    //happy path
    public class WhenTheOutwardCodeServiceIsCalled_WithValidPostcode
    {
        OutwardCodeService _outwardCodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _outwardCodeService = new OutwardCodeService();
            await _outwardCodeService.MakeRequestAsync("EC2Y");
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_outwardCodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void CorrectOutcodeIsReturned()
        {
            var result = _outwardCodeService.ResponseContent["result"]["outcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y"));
        }

        [Test]
        public void AdminDistrictAtIndexOne_IsCityOfLondon()
        {
            Assert.That(_outwardCodeService.OutwardCodeDTO.OutwardCodeResponse.result.admin_district[1], Is.EqualTo("City of London"));
        }

        [Test]
        public void Northings_Is181778()
        {
            Assert.That(_outwardCodeService.OutwardCodeDTO.OutwardCodeResponse.result.northings, Is.EqualTo(181778));
        }
    }
}
