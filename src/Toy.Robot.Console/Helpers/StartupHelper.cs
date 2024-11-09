using Microsoft.Extensions.DependencyInjection;
using Toy.Robot.Console.Const;
using Toy.Robot.Console.Models;
using Toy.Robot.Console.Services;

namespace Toy.Robot.Console.Helpers;

public static class StartupHelper
{
    public static ServiceProvider BuildServicePrivider()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRobotService, RobotService>();
        services.AddSingleton(new RobotPosition 
        {
            X = 0,
            Y = 0,
            Face = RobotFace.NORTH
        });

        return services.BuildServiceProvider();
    }

    public static RobotPosition? ExtractRobotPosition(string stringArgument)
    {
        var placeParameters = stringArgument.Split(',');

        if (placeParameters.Length != 3)
        { 
            return null;
        }

        bool canParseX = uint.TryParse(placeParameters[0], out var x);
        bool canParseY = uint.TryParse(placeParameters[1], out var y);
        bool canParseFace = Enum.TryParse<RobotFace>(placeParameters[2], out var face);

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
