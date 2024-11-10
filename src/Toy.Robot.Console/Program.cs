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
        var services = new ServiceCollection();
        services.AddRobotServices();
        using var serviceProvider = services.BuildServiceProvider();

        var robotService = serviceProvider.GetService<IRobotService>();
        ArgumentNullException.ThrowIfNull(robotService);

        var availableCommands = typeof(RobotCommand).GetFields().Select(f => f.Name);
        System.Console.WriteLine($"Available commands: {string.Join(", ", availableCommands)}");

        string command = string.Empty;

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
                    robotService.TurnLeft();
                    break;
                case RobotCommand.Right:
                    robotService.TurnRight();
                    break;
                case RobotCommand.Report:
                    robotService.PrintReport();
                    break;
                case RobotCommand.Exit:
                    break;
                default:
                    break;
            }
        } 
    }
}
