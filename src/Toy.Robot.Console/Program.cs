using Microsoft.Extensions.DependencyInjection;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Helpers;
using Toy.Robot.Console.Services;

namespace Toy.Robot.Console;

public class Program
{
    protected Program() 
    { 
    }

    static void Main(string[] args)
    {
        using var serviceProvider = StartupHelper.BuildServicePrivider();
        var robotService = serviceProvider.GetService<IRobotService>() ??
            throw new Exception();

        string command = string.Empty;

        while (!string.Equals(command, RobotCommand.Exit, StringComparison.InvariantCultureIgnoreCase))
        {
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
                    var position = StartupHelper.ExtractRobotPosition(arguments[argumentsLength - 1]);
                    if (position == null)
                    {
                        break;
                    }
                    robotService.Place(position);
                    break;
                case RobotCommand.Move:
                    robotService.Move();
                    break;
                case RobotCommand.Left:
                    robotService.Left();
                    break;
                case RobotCommand.Right:
                    robotService.Right();
                    break;
                case RobotCommand.Report:
                    robotService.Report();
                    break;
                case RobotCommand.Exit:
                    break;
                default:
                    break;
            }
        } 
    }
}
