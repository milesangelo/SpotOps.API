using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SpotOps.Api.Controllers;
using SpotOps.Api.Models.Rest;
using SpotOps.Api.Services;

namespace SpotOps.Test.Api.Controllers;

[TestFixture]
internal class SpotControllerTests
{
    [Test]
    public void Post_When_CalledWithValidFormData_Should_ReturnOkObjectResult()
    {
        var spotController = new SpotsController(new MockSpotResponseService());

        var spotResponse = new SpotRequest
        {
            Id = 1,
            Name = "test",
            Type = "testtype"
        };

        var actionResult = spotController.Post(spotResponse).Result;
        var okResult = actionResult as OkObjectResult;

        Assert.IsNotNull(actionResult);
        Assert.AreEqual(200, okResult?.StatusCode);
    }
}