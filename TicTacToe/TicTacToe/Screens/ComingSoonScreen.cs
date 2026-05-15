using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class ComingSoonScreen : Screen
{
    private readonly Renderer _renderer;
    private readonly NavigationManager _navigation;

    public override bool HandlesOwnInput => true;

    public ComingSoonScreen(Renderer renderer, NavigationManager navigation)
    {
        _renderer = renderer;
        _navigation = navigation;
    }

    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawHeader("Player vs Computer");
        _renderer.DrawSection("Coming soon");
        _renderer.DrawMuted("This game mode is not available yet.");
        _renderer.DrawFooter("[Enter] Back   [Backspace] Back   [ESC] Quit");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Enter)
            {
                _navigation.GoBack();
                return;
            }
        }
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
    }
}
