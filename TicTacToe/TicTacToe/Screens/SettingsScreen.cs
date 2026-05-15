using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class SettingsScreen : Screen
{
    private readonly AppState _appState;
    private readonly NavigationManager _navigationManager;
    private readonly Renderer _renderer;

    public override bool HandlesOwnInput => true;
    
    public SettingsScreen(AppState appState, NavigationManager navigationManager, Renderer renderer)
    {
        _appState = appState;
        _navigationManager = navigationManager;
        _renderer = renderer;
    }

    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawText("Settings");
        _renderer.DrawText($"Current username: " + _appState.Username);
        Console.WriteLine("Enter new username(or press Enter to keep current): ");
        string? username = Console.ReadLine();
        if (!string.IsNullOrEmpty(username)) _appState.Username = username;
        _navigationManager.GoBack();
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        // empty
    }

}