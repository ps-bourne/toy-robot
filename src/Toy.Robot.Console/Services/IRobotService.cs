using Toy.Robot.Console.Enum;

namespace Toy.Robot.Console.Services;

public interface IRobotService
{
    void Place(int x, int y, RobotFace face);

    void Move();

    void Left();

    void Right();

    void Report();
}
