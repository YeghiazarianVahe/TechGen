using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class PlayMenuScreen : MenuScreen
{
    public PlayMenuScreen(
        AppState appState,
        NavigationManager navigation,
        Renderer renderer,
        IScreen symbolSelectionScreen,
        IScreen comingSoonScreen)
        : base(
            "Play",
            new MenuItem[]
            {
                new MenuItem(
                    "Player vs Player",
                    new SetGameModeCommand(
                        appState,
                        GameMode.PlayerVsPlayer,
                        navigation,
                        symbolSelectionScreen)),
                new MenuItem(
                    "Player vs Computer",
                    new NavigateCommand(navigation, comingSoonScreen))
            },
            renderer,
            navigation)
    {
    }
}
