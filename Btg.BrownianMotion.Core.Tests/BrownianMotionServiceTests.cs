using Btg.BrownianMotion.Core.Services;

namespace Btg.BrownianMotion.Core.Tests;

[TestClass]
public sealed class BrownianMotionServiceTests
{
    [TestMethod]
    public void GenerateBrownianMotion_Should_Not_Return_Null()
    {
        //Arrange
        var brownianMotionService = new BrownianMotionService();

        //Act
        var result = brownianMotionService.GenerateBrownianMotion(1, 2, 3, 4);

        //Assert
        Assert.IsNotNull(result);
    }
}