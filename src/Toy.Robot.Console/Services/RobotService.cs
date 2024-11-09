using Toy.Robot.Console.Enum;

namespace Toy.Robot.Console.Services;

public class RobotService: IRobotService
{
    public void Place(int x, int y, RobotFace face)
    {
        System.Console.WriteLine("Place command executed.");
    }

    public void Move()
    {
        System.Console.WriteLine("Move command executed.");
    }

    public void Left()
    {
        System.Console.WriteLine("Left command executed.");
    }

    public void Right()
    {
        System.Console.WriteLine("Right command executed.");
    }

    public void Report()
    {
        System.Console.WriteLine("Report command executed.");
    }
}
