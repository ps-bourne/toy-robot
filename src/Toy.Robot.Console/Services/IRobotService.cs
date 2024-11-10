using Toy.Robot.Console.Models;

namespace Toy.Robot.Console.Services;

public interface IRobotService
{
    void Place(RobotPosition robotPosition);

    void Move();

    void TurnLeft();

    void TurnRight();

    void PrintReport();
}
