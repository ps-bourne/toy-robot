using Microsoft.Extensions.DependencyInjection;
using Toy.Robot.Console.Services;

namespace Toy.Robot.Console.Helpers;

public static class ServiceHelper
{
    public static ServiceProvider BuildServicePrivider()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRobotService, RobotService>();

        return services.BuildServiceProvider();
    }
}
