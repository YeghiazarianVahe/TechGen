using MenuLib.Core;
using MenuLib.Navigation;
using TicTacToe.Game;

namespace TicTacToe.Screens;

public class SetSymbolCommand : ICommand
{
    private readonly AppState _appState;
    private readonly CellState _symbol;
    private readonly NavigationManager _navigation;
    private readonly GameScreen _gameScreen;

    public SetSymbolCommand(
        AppState appState,
        CellState symbol,
        NavigationManager navigation,
        GameScreen gameScreen)
    {
        _appState = appState;
        _symbol = symbol;
        _navigation = navigation;
        _gameScreen = gameScreen;
    }

    public void Execute()
    {
        _appState.PlayerSymbol = _symbol;
        _appState.OpponentSymbol = _symbol == CellState.X ? CellState.O : CellState.X;

        Player player1 = new Player(_appState.Username, _appState.PlayerSymbol, PlayerType.Human);
        Player player2 = new Player("Player 2", _appState.OpponentSymbol, PlayerType.Human);

        _appState.Engine.StartGame(player1, player2);
        _gameScreen.ResetState();
        _navigation.GoTo(_gameScreen);
    }
}
