using Microsoft.Extensions.DependencyInjection;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;
using Toy.Robot.Console.Services;

namespace Toy.Robot.Console.Helpers;

public static class StartupHelper
{
    public static void AddRobotServices(this ServiceCollection services)
    {
        services.AddSingleton<IRobotService, RobotService>();

        services.AddSingleton(new RobotPosition());

        services.AddSingleton<IConsoleService, ConsoleService>();
    }

    public static RobotPosition? ExtractRobotPosition(string stringArgument)
    {
        var placeParameters = stringArgument.Split(',');

        if (placeParameters.Length != 3)
        { 
            return null;
        }

        bool canParseX = int.TryParse(placeParameters[0].Trim(), out var x);
        bool canParseY = int.TryParse(placeParameters[1].Trim(), out var y);
        bool canParseFace = Enum.TryParse<RobotFace>(placeParameters[2].Trim(), out var face);

        if (!canParseX || !canParseY || !canParseFace)
        {
            return null;
        }

        return new RobotPosition
        {
            X = x,
            Y = y,
            Face = face
        };
    }
}
