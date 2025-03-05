namespace Codeacula.Core.Tests.Models;

using Codeacula.Core.Domain.Users.Models;

public class UserTests
{
  [Fact]
  public void User_Id_ShouldBeSettableAndGettable()
  {
    // Arrange
    var expectedId = Guid.NewGuid();
    var user = new User
    {
      Id = expectedId,
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };

    // Act
    var actualId = user.Id;

    // Assert
    Assert.Equal(expectedId, actualId);
  }

  [Fact]
  public void User_DisplayName_ShouldBeSettableAndGettable()
  {
    // Arrange
    const string expectedName = "Test User";
    var user = new User
    {
      Id = Guid.NewGuid(),
      DisplayName = expectedName,
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };

    // Act
    var actualName = user.DisplayName;

    // Assert
    Assert.Equal(expectedName, actualName);
  }

  [Fact]
  public void User_Email_ShouldBeSettableAndGettable()
  {
    // Arrange
    const string expectedEmail = "test@example.com";
    var user = new User
    {
      Id = Guid.NewGuid(),
      DisplayName = "Test User",
      Email = expectedEmail,
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };

    // Act
    var actualEmail = user.Email;

    // Assert
    Assert.Equal(expectedEmail, actualEmail);
  }

  [Fact]
  public void User_ProfileImageUrl_ShouldBeSettableAndGettable()
  {
    // Arrange
    const string expectedUrl = "https://example.com/image.jpg";
    var user = new User
    {
      Id = Guid.NewGuid(),
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = expectedUrl,
      Username = "testuser",
    };

    // Act
    var actualUrl = user.ProfileImageUrl;

    // Assert
    Assert.Equal(expectedUrl, actualUrl);
  }

  [Fact]
  public void User_Username_ShouldBeSettableAndGettable()
  {
    // Arrange
    const string expectedUsername = "testuser";
    var user = new User
    {
      Id = Guid.NewGuid(),
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = expectedUsername,
    };

    // Act
    var actualUsername = user.Username;

    // Assert
    Assert.Equal(expectedUsername, actualUsername);
  }

  [Fact]
  public void User_OrbEssence_ShouldDefaultToZero()
  {
    // Arrange
    var user = new User
    {
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };

    // Assert
    Assert.Equal(0, user.OrbEssence);
  }

  [Fact]
  public void User_Roles_ShouldInitializeToEmptyCollection()
  {
    // Arrange
    var user = new User
    {
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };

    // Assert
    Assert.NotNull(user.Roles);
    Assert.Empty(user.Roles);
  }

  [Fact]
  public void User_Roles_ShouldAllowAddingRoles()
  {
    // Arrange
    var user = new User
    {
      DisplayName = "Test User",
      Email = "test@example.com",
      ProfileImageUrl = "https://example.com/image.jpg",
      Username = "testuser",
    };
    var role = new Role { Id = Guid.NewGuid(), Name = "TestRole" };

    // Act
    user.Roles.Add(role);

    // Assert
    _ = Assert.Single(user.Roles);
    Assert.Contains(role, user.Roles);
  }
}
