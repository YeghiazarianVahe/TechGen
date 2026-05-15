using TicTacToe.Game;

namespace TicTacToe;

public class AppState
{
    public string Username { get; set; }
    public GameMode GameMode { get; set; }
    public CellState PlayerSymbol { get; set; }
    public CellState OpponentSymbol { get; set; }
    public GameEngine Engine { get; set; }

    public AppState()
    {
        Username = "Player";
        GameMode = GameMode.PlayerVsPlayer;
        PlayerSymbol = CellState.X;
        OpponentSymbol = CellState.O;
        Engine = new GameEngine();
    }
}
