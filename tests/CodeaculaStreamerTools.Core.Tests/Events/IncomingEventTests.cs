namespace CodeaculaStreamerTools.Core.Tests.Events;

using System.ComponentModel.DataAnnotations;
using CodeaculaStreamerTools.Core.Common.ES;

public class IncomingEventTests
{
  private static IncomingEvent CreateValidEvent() => new()
  {
    AggregateId = Guid.NewGuid(),
    AggregateType = "TestAggregate",
    EventType = "TestEvent",
    Data = /*lang=json,strict*/ "{\"test\": \"data\"}",
    Version = 1,
  };

  [Fact]
  public void Validation_WithValidData_ShouldPass()
  {
    // Arrange
    var evt = CreateValidEvent();

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.True(isValid);
    Assert.Empty(results);
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  public void Validation_WithInvalidAggregateType_ShouldFail(string? invalidValue)
  {
    // Arrange
    var evt = CreateValidEvent() with { AggregateType = invalidValue! };

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(results, r => r.MemberNames.Contains(nameof(IncomingEvent.AggregateType)));
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  public void Validation_WithInvalidEventType_ShouldFail(string? invalidValue)
  {
    // Arrange
    var evt = CreateValidEvent() with { EventType = invalidValue! };

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(results, r => r.MemberNames.Contains(nameof(IncomingEvent.EventType)));
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  public void Validation_WithInvalidData_ShouldFail(string? invalidValue)
  {
    // Arrange
    var evt = CreateValidEvent() with { Data = invalidValue! };

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(results, r => r.MemberNames.Contains(nameof(IncomingEvent.Data)));
  }

  [Fact]
  public void Record_WithSameValues_ShouldBeEqual()
  {
    // Arrange
    var evt1 = CreateValidEvent();
    var evt2 = evt1 with { };

    // Assert
    Assert.Equal(evt1, evt2);
  }

  [Fact]
  public void Record_WithDifferentValues_ShouldNotBeEqual()
  {
    // Arrange
    var evt1 = CreateValidEvent();
    var evt2 = evt1 with { AggregateId = Guid.NewGuid() };

    // Assert
    Assert.NotEqual(evt1, evt2);
  }

  [Theory]
  [InlineData(101, nameof(IncomingEvent.AggregateType))]
  [InlineData(201, nameof(IncomingEvent.EventType))]
  public void Validation_StringLengthExceeded_ShouldFail(int length, string propertyName)
  {
    // Arrange
    var longString = new string('x', length);
    var evt = propertyName switch
    {
      nameof(IncomingEvent.AggregateType) => CreateValidEvent() with { AggregateType = longString },
      nameof(IncomingEvent.EventType) => CreateValidEvent() with { EventType = longString },
      _ => throw new ArgumentException($"Invalid property name: {propertyName}"),
    };

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(results, r => r.MemberNames.Contains(propertyName));
  }

  [Fact]
  public void Metadata_CanBeNull_ShouldPass()
  {
    // Arrange
    var evt = CreateValidEvent() with { Metadata = null };

    // Act
    var context = new ValidationContext(evt);
    var results = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(evt, context, results, true);

    // Assert
    Assert.True(isValid);
    Assert.Empty(results);
  }

  [Fact]
  public void Metadata_WithValue_ShouldBePreserved()
  {
    // Arrange
    const string metadata = /*lang=json,strict*/ "{\"source\": \"test\"}";
    var evt = CreateValidEvent() with { Metadata = metadata };

    // Assert
    Assert.Equal(metadata, evt.Metadata);
  }
}
