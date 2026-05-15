using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class SetGameModeCommand : ICommand
{
    private readonly AppState _appState;
    private readonly GameMode _gameMode;
    private readonly NavigationManager _navigation;
    private readonly IScreen _nextScreen;

    public SetGameModeCommand(
        AppState appState,
        GameMode gameMode,
        NavigationManager navigation,
        IScreen nextScreen)
    {
        _appState = appState;
        _gameMode = gameMode;
        _navigation = navigation;
        _nextScreen = nextScreen;
    }

    public void Execute()
    {
        _appState.GameMode = _gameMode;
        _navigation.GoTo(_nextScreen);
    }
}
