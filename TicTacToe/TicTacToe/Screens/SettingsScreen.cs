using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class SettingsScreen : Screen
{
    private readonly AppState _appState;
    private readonly NavigationManager _navigationManager;
    private readonly Renderer _renderer;
    private string _newUsername = "";

    public override bool HandlesOwnInput => true;
    
    public SettingsScreen(AppState appState, NavigationManager navigationManager, Renderer renderer)
    {
        _appState = appState;
        _navigationManager = navigationManager;
        _renderer = renderer;
    }

    public override void Render()
    {
        while (true)
        {
            DrawSettings();
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                if (!string.IsNullOrEmpty(_newUsername))
                {
                    _appState.Username = _newUsername;
                }

                _newUsername = "";
                _navigationManager.GoBack();
                return;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (_newUsername.Length == 0)
                {
                    _navigationManager.GoBack();
                    return;
                }

                _newUsername = _newUsername.Substring(0, _newUsername.Length - 1);
            }
            else if (!char.IsControl(key.KeyChar))
            {
                _newUsername += key.KeyChar;
            }
        }
    }

    private void DrawSettings()
    {
        _renderer.Clear();
        _renderer.DrawHeader("Settings");
        _renderer.DrawLabelValue("Current username: ", _appState.Username);
        _renderer.DrawText("");
        _renderer.DrawSection("New username");
        Console.Write("> ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(_newUsername);
        Console.ResetColor();
        _renderer.DrawText("");
        _renderer.DrawFooter("[Enter] Save   [Backspace] Delete / Back   [ESC] Quit");
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
    }

}
