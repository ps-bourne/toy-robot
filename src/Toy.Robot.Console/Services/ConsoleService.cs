namespace Toy.Robot.Console.Services;

public class ConsoleService : IConsoleService
{
    public void WriteLine(string message)
    {
        System.Console.WriteLine(message);
    }
}
