using NUnit.Framework;
using SpotOps.Api.Controllers;

namespace SpotOps.Test.Api.Controllers;

[TestFixture]
public class PingControllerTests
{
    [Test]
    public void Get_When_Pinged_Should_ReturnPonged()
    {
        var pingController = new PingController();

        var message = pingController.Get();
        
        Assert.AreEqual(message, "pong");
    }
}