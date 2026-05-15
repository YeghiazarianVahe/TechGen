using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class AboutScreen : Screen
{
    private readonly Renderer _renderer;
    private readonly NavigationManager _navigation;
    private readonly AppState _appState;
    
    public AboutScreen(
        Renderer renderer,
        NavigationManager navigation,
        AppState appState)
    {
        _renderer = renderer;
        _navigation = navigation;
        _appState = appState;
    }
    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawText("About");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Player: {_appState.Username}");
        Console.WriteLine("TechGen C# course");
        Console.WriteLine("May 2026");
        Console.ResetColor();          
        Console.WriteLine("Press any key...");
        Console.ReadKey(true);
        _navigation.GoBack();
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        // empty
    }
}