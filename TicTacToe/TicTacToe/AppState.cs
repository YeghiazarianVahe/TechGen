using TicTacToe.Game;

namespace TicTacToe;

public class AppState
{
    public string Username { get; set; }
    public GameMode GameMode { get; set; }
    public GameEngine Engine { get; set; }

    public AppState()
    {
        Engine = new GameEngine();
    }
}