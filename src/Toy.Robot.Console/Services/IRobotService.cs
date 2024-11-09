using Toy.Robot.Console.Enum;
using Toy.Robot.Console.Models;

namespace Toy.Robot.Console.Services;

public interface IRobotService
{
    void Place(RobotPosition robotPosition);

    void Move();

    void Left();

    void Right();

    void Report();
}
