namespace TicTacToe.Game;

public class Player
{
    public string Name { get; }
    public CellState Symbol { get;  }
    public PlayerType PlayerType { get; }

    public Player(string name, CellState symbol, PlayerType playerType)
    {
        Name = name;
        Symbol = symbol;
        PlayerType = playerType;
    }

}