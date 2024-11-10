using Moq;
using Shouldly;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;
using Toy.Robot.Console.Services;

namespace Toy.Robot.Test.Services;

public class RobotService_Tests
{
    private readonly RobotService _robotService;
    private readonly RobotPosition _robotPosition;
    private readonly Mock<IConsoleService> _mockConsoleService;

    public RobotService_Tests()
    {
        _robotPosition = new RobotPosition();

        _mockConsoleService = new Mock<IConsoleService>();

        _robotService = new RobotService(_robotPosition, _mockConsoleService.Object);
    }

    [Fact]
    public void Place_ShouldSetRobotPosition()
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = 1,
            Y = 2,
            Face = RobotFace.EAST
        };

        // Act
        _robotService.Place(initPlace);

        // Assert
        _robotPosition.ShouldBe(initPlace);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    [InlineData(5, 5)]
    public void Place_ShouldNotPlaceOutsideTable(int x ,int y)
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = x,
            Y = y,
            Face = RobotFace.NORTH
        };

        // Act
        _robotService.Place(initPlace);

        // Assert
        _robotPosition.IsOnTable.ShouldBeFalse();
    }

    [Theory]
    [InlineData(RobotFace.NORTH, 0, 0, 0, 1)]
    [InlineData(RobotFace.EAST, 0, 0, 1, 0)]
    public void Move_ShouldUpdateRobotPosition(
        RobotFace currentFace,
        int currentX,
        int currentY,
        int expectedX,
        int expectedY)
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = currentX,
            Y = currentY,
            Face = currentFace
        };

        var expectedPlace = initPlace with { X = expectedX, Y = expectedY };

        // Act
        _robotService.Place(initPlace);
        _robotService.Move();

        // Assert
        _robotPosition.ShouldBe(expectedPlace);
    }

    [Fact]
    public void Move_ShouldIgnoreIfNotPlaced()
    {
        // Act
        _robotService.Move();

        // Assert
        _robotPosition.IsOnTable.ShouldBeFalse();
    }

    [Theory]
    [InlineData(RobotFace.WEST, 0, 0)]
    [InlineData(RobotFace.SOUTH, 0, 0)]
    [InlineData(RobotFace.NORTH, 4, 4)]
    [InlineData(RobotFace.EAST, 4, 4)]
    public void Move_ShouldNotFallOffTable(RobotFace currentFace, int x, int y)
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = x,
            Y = y,
            Face = currentFace
        };

        // Act
        _robotService.Place(initPlace);
        _robotService.Move();

        // Assert
        _robotPosition.ShouldBe(initPlace);
    }

    [Theory]
    [InlineData(RobotFace.NORTH, RobotFace.WEST)]
    [InlineData(RobotFace.WEST, RobotFace.SOUTH)]
    [InlineData(RobotFace.SOUTH, RobotFace.EAST)]
    [InlineData(RobotFace.EAST, RobotFace.NORTH)]
    public void TurnLeft_ShouldUpdateRobotFace(RobotFace currentFace, RobotFace expectedFace)
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = 0,
            Y = 0,
            Face = currentFace
        };

        var expectedPlace = initPlace with { Face = expectedFace };

        // Act
        _robotService.Place(initPlace);
        _robotService.TurnLeft();

        // Assert
        _robotPosition.ShouldBe(expectedPlace);
    }

    [Fact]
    public void TurnLeft_ShouldIgnoreIfNotPlaced()
    {
        // Act
        _robotService.TurnLeft();

        // Assert
        _robotPosition.IsOnTable.ShouldBeFalse();
    }

    [Theory]
    [InlineData(RobotFace.NORTH, RobotFace.EAST)]
    [InlineData(RobotFace.EAST, RobotFace.SOUTH)]
    [InlineData(RobotFace.SOUTH, RobotFace.WEST)]
    [InlineData(RobotFace.WEST, RobotFace.NORTH)]
    public void TurnRight_ShouldUpdateRobotFace(RobotFace currentFace, RobotFace expectedFace)
    {
        // Arrange
        var initPlace = new RobotPosition()
        {
            X = 0,
            Y = 0,
            Face = currentFace
        };

        var expectedPlace = initPlace with { Face = expectedFace };

        // Act
        _robotService.Place(initPlace);
        _robotService.TurnRight();

        // Assert
        _robotPosition.ShouldBe(expectedPlace);
    }

    [Fact]
    public void Right_ShouldIgnoreIfNotPlaced()
    {
        // Act
        _robotService.TurnRight();

        // Assert
        _robotPosition.IsOnTable.ShouldBeFalse();
    }

    [Fact]
    public void Report_ShouldWriteRobotPosition()
    {
        // Arrange
        var x = 0;
        var y = 0;
        var face = RobotFace.NORTH;

        var initPlace = new RobotPosition()
        {
            X = x,
            Y = y,
            Face = face
        };
        string callbackString = string.Empty;

        _mockConsoleService
            .Setup(x => x.WriteLine(It.IsAny<string>()))
            .Callback((string line) => { callbackString = line; });

        // Act
        _robotService.Place(initPlace);
        _robotService.PrintReport();

        // Assert
        callbackString.ShouldBe($"Output: {x}, {y}, {face}");
    }

    [Fact]
    public void Report_ShouldIgnoreIfNotPlaced()
    {
        // Act
        _robotService.PrintReport();

        // Assert
        _mockConsoleService.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never);
    }
}