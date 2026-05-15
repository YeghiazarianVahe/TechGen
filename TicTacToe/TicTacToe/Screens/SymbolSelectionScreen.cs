using MenuLib.Core;
using MenuLib.Navigation;
using TicTacToe.Game;

namespace TicTacToe.Screens;

public class SymbolSelectionScreen : MenuScreen
{
    public SymbolSelectionScreen(
        AppState appState,
        NavigationManager navigation,
        Renderer renderer,
        GameScreen gameScreen)
        : base(
            "Choose Symbol",
            new MenuItem[]
            {
                new MenuItem(
                    "Play as X",
                    new SetSymbolCommand(
                        appState,
                        CellState.X,
                        navigation,
                        gameScreen)),
                new MenuItem(
                    "Play as O",
                    new SetSymbolCommand(
                        appState,
                        CellState.O,
                        navigation,
                        gameScreen))
            },
            renderer,
            navigation)
    {
    }
}
