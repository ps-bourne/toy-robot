using System.ComponentModel.DataAnnotations;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;

namespace Toy.Robot.Console.Services;

public class RobotService : IRobotService
{
    private readonly RobotPosition _robotPlace;
    private readonly IConsoleService _consoleService;

    public RobotService(RobotPosition toyRobot, IConsoleService consoleService)
    {
        _robotPlace = toyRobot;
        _consoleService = consoleService;
    }

    public void Place(RobotPosition robotPosition)
    {
        bool isValidPosition = Validator.TryValidateObject(robotPosition, new ValidationContext(robotPosition), null, true);

        if (!isValidPosition)
        {
            return;
        }

        _robotPlace.X = robotPosition.X;
        _robotPlace.Y = robotPosition.Y;
        _robotPlace.Face = robotPosition.Face;

    }

    public void Move()
    {
        if (!_robotPlace.IsOnTable)
        {
            return;
        }

        switch (_robotPlace.Face)
        {
            case RobotFace.NORTH:
                if (_robotPlace.Y < TableConst.MaxY)
                {
                    _robotPlace.Y++;
                }
                break;
            case RobotFace.EAST:
                if (_robotPlace.X < TableConst.MaxX)
                {
                    _robotPlace.X++;
                }
                break;
            case RobotFace.SOUTH:
                if (_robotPlace.Y > 0)
                {
                    _robotPlace.Y--;
                }
                break;
            case RobotFace.WEST:
                if (_robotPlace.X > 0)
                {
                    _robotPlace.X--;
                }
                break;
        }
    }

    public void TurnLeft()
    {
        if (!_robotPlace.IsOnTable)
        {
            return;
        }

        int currentFace = (int)_robotPlace.Face!;
        int nextFace = (currentFace + 3) % 4;
        _robotPlace.Face = (RobotFace)nextFace;
    }

    public void TurnRight()
    {
        if (!_robotPlace.IsOnTable)
        {
            return;
        }

        int currentFace = (int)_robotPlace.Face!;
        int nextFace = (currentFace + 1) % 4;
        _robotPlace.Face = (RobotFace)nextFace;
    }

    public void PrintReport()
    {
        if (!_robotPlace.IsOnTable)
        {
            return;
        }

        _consoleService.WriteLine($"Output: {_robotPlace.X}, {_robotPlace.Y}, {_robotPlace.Face}");
    }
}
