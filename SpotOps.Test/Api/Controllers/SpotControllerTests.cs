using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SpotOps.Api.Controllers;
using SpotOps.Api.Models.Rest;
using SpotOps.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpotOps.Test.Api.Controllers
{
    [TestFixture]
    internal class SpotControllerTests
    {
        [Test]
        public void Post_When_CalledWithValidFormData_Should_ReturnOkObjectResult()
        {
            var spotController = new SpotsController(new MockSpotResponseService());

            var spotResponse = new SpotRequest() 
            { 
                Name = "test",
                DateCreated = DateTime.Now,
                Id = 1,
                Type = "testtype"
            };

            var actionResult = spotController.Post(spotResponse).Result;
            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
