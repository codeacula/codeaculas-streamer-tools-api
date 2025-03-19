namespace CodeaculaStreamerTools.Core.Tests.Models;

using CodeaculaStreamerTools.Core.Domain.Users.Models;

public class RoleTests
{
  [Fact]
  public void Role_Id_ShouldBeSettableAndGettable()
  {
    // Arrange
    var expectedId = Guid.NewGuid();
    var role = new Role { Id = expectedId, Name = "Test Role" };

    // Act
    var actualId = role.Id;

    // Assert
    Assert.Equal(expectedId, actualId);
  }

  [Fact]
  public void Role_Name_ShouldBeSettableAndGettable()
  {
    // Arrange
    const string expectedName = "Admin";
    var role = new Role { Id = Guid.NewGuid(), Name = expectedName };

    // Act
    var actualName = role.Name;

    // Assert
    Assert.Equal(expectedName, actualName);
  }
}
