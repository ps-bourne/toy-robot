using Toy.Robot.Console.Const;

namespace Toy.Robot.Console.Models;

public record RobotPosition
{
    public uint X { get; set; }

    public uint Y { get; set; }

    public RobotFace Face { get; set; }
}
