using Toy.Robot.Console.Const;
using Toy.Robot.Console.Helpers;
using Toy.Robot.Console.Models;

namespace Toy.Robot.Console;

public class Program
{
    protected Program()
    {
    }

    static void Main(string[] args)
    {
        var availableCommands = typeof(RobotCommand).GetFields().Select(f => f.Name);
        System.Console.WriteLine($"Available commands: {string.Join(", ", availableCommands)}");

        string command = string.Empty;
        var robot = new RobotPosition();

        while (!string.Equals(command, RobotCommand.Exit, StringComparison.InvariantCultureIgnoreCase))
        {
            System.Console.Write('>');

            string line = System.Console.ReadLine()?.Trim() ?? string.Empty;

            string[] arguments = line.Split(' ');

            command = arguments[0];

            switch (command.ToUpper())
            {
                case RobotCommand.Place:
                    int argumentsLength = arguments.Length;
                    if (argumentsLength <= 1)
                    {
                        break;
                    }
                    var position = RobotHelper.ExtractRobotPosition(arguments[argumentsLength - 1]);
                    if (position == null)
                    {
                        break;
                    }
                    robot.Place(position);
                    break;
                case RobotCommand.Move:
                    robot.Move();
                    break;
                case RobotCommand.Left:
                    robot.TurnLeft();
                    break;
                case RobotCommand.Right:
                    robot.TurnRight();
                    break;
                case RobotCommand.Report:
                    robot.PrintReport();
                    break;
                case RobotCommand.Exit:
                    break;
                default:
                    break;
            }
        }
    }
}
