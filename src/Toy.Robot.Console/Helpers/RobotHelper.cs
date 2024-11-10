using System.ComponentModel.DataAnnotations;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;

namespace Toy.Robot.Console.Helpers;

public static class RobotHelper
{
    public static RobotPosition? ExtractRobotPosition(string stringArgument)
    {
        var placeParameters = stringArgument.Split(',');

        if (placeParameters.Length != 3)
        {
            return null;
        }

        bool canParseX = int.TryParse(placeParameters[0].Trim(), out var x);
        bool canParseY = int.TryParse(placeParameters[1].Trim(), out var y);
        bool canParseFace = Enum.TryParse<RobotFace>(placeParameters[2].Trim(), out var face);

        if (!canParseX || !canParseY || !canParseFace)
        {
            return null;
        }

        return new RobotPosition
        {
            X = x,
            Y = y,
            Face = face
        };
    }

    public static void Place(this RobotPosition originalPosition, RobotPosition newPosition)
    {
        bool isValidPosition = Validator.TryValidateObject(newPosition, new ValidationContext(newPosition), null, true);

        if (!isValidPosition)
        {
            return;
        }

        originalPosition.X = newPosition.X;
        originalPosition.Y = newPosition.Y;
        originalPosition.Face = newPosition.Face;

    }

    public static void Move(this RobotPosition robotPosition)
    {
        if (!robotPosition.IsOnTable)
        {
            return;
        }

        switch (robotPosition.Face)
        {
            case RobotFace.NORTH:
                if (robotPosition.Y < TableConst.MaxY)
                {
                    robotPosition.Y++;
                }
                break;
            case RobotFace.EAST:
                if (robotPosition.X < TableConst.MaxX)
                {
                    robotPosition.X++;
                }
                break;
            case RobotFace.SOUTH:
                if (robotPosition.Y > 0)
                {
                    robotPosition.Y--;
                }
                break;
            case RobotFace.WEST:
                if (robotPosition.X > 0)
                {
                    robotPosition.X--;
                }
                break;
        }
    }

    public static void TurnLeft(this RobotPosition robotPosition)
    {
        if (!robotPosition.IsOnTable)
        {
            return;
        }

        int currentFace = (int)robotPosition.Face!;
        int nextFace = (currentFace + 3) % 4;
        robotPosition.Face = (RobotFace)nextFace;
    }

    public static void TurnRight(this RobotPosition robotPosition)
    {
        if (!robotPosition.IsOnTable)
        {
            return;
        }

        int currentFace = (int)robotPosition.Face!;
        int nextFace = (currentFace + 1) % 4;
        robotPosition.Face = (RobotFace)nextFace;
    }

    public static void PrintReport(this RobotPosition robotPosition)
    {
        if (!robotPosition.IsOnTable)
        {
            return;
        }

        System.Console.WriteLine($"Output: {robotPosition.X}, {robotPosition.Y}, {robotPosition.Face}");
    }
}
