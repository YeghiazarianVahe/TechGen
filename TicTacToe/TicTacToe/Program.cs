using MenuLib.Core;
using MenuLib.Navigation;

namespace MenuLib;

public class Program
{
    public static void Main(string[] args)
    {
        Renderer renderer = new Renderer();
        InputReader input =  new InputReader();
        NavigationManager navigation = new NavigationManager();
        MenuItem[] items = new MenuItem[]
        {
            new MenuItem("Say Hello", new SayHelloCommand()),
            new  MenuItem("Settings", new OpenSettings()),
            new MenuItem("Quit", new QuitCommand())
        };

        MenuScreen menu = new MenuScreen("Menu", items, renderer, navigation);
        navigation.GoTo(menu);
        while (true)
        {
            navigation.CurrentScreen.Render();
            ConsoleKeyInfo key = input.ReadKey();
            navigation.CurrentScreen.HandleInput(key);
        }
    }
}

class SayHelloCommand : ICommand
{
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Hello World!");
        Console.ReadKey(true);  
    }
}

class QuitCommand : ICommand
{
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Bye!");
        Environment.Exit(0);
    }
}

class OpenSettings : ICommand
{
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Open Settings");
        Console.ReadKey(true);
    }
}