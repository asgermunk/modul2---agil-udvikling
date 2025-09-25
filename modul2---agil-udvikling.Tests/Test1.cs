namespace modul2___agil_udvikling.Tests;

using modul2_agiludvikling.Services;
using modul2_agiludvikling.Controllers;
// Update the namespace below to match the actual location of your services


using Moq;
[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.IsTrue(true);
    }
}
[TestClass]
public class createUserNullInput{
    [TestMethod]
    public void CreateUser_NullInput_ReturnsBadRequest()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();

        var controller = new UsersController(mockUserService.Object);

        // Act
        var result = controller.CreateUser(null);
        var user = result.Result;

        // Assert
        Assert.IsNull(result.Value);
        Assert.AreEqual(400, result.StatusCode);
        Assert.AreEqual(0, user.Count());
    }
}