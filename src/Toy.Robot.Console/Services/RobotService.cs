using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;

namespace Toy.Robot.Console.Services;

public class RobotService : IRobotService
{
    private readonly RobotPosition _robotPlace;

    public RobotService(RobotPosition toyRobot)
    {
        _robotPlace = toyRobot;
    }

    public void Place(RobotPosition robotPosition)
    {
        _robotPlace.X = robotPosition.X;
        _robotPlace.Y = robotPosition.Y;
        _robotPlace.Face = robotPosition.Face;
    }

    public void Move()
    {
        switch (_robotPlace.Face)
        {
            case RobotFace.NORTH:
                _robotPlace.Y++;
                break;
            case RobotFace.EAST:
                _robotPlace.X++;
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

    public void Left()
    {
        int currentFace = (int)_robotPlace.Face;
        int nextFace = (currentFace + 3) % 4;
        _robotPlace.Face = (RobotFace)nextFace;
    }

    public void Right()
    {
        int currentFace = (int)_robotPlace.Face;
        int nextFace = (currentFace + 1) % 4;
        _robotPlace.Face = (RobotFace)nextFace;
    }

    public void Report()
    {
        System.Console.WriteLine($"Output: {_robotPlace.X}, {_robotPlace.Y}, {_robotPlace.Face}");
    }
}
