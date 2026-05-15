namespace TicTacToe.Game;

public class GameEngine
{
    private Board _board;
    private WinnerChecker _checker;
    private Player[] _player;
    private int _currentPlayerIndex;

    public GameEngine()
    {
        _board = new Board();
        _checker = new WinnerChecker();
        _player = new Player[]
        {
            new Player("Player 1", CellState.X, PlayerType.Human),
            new Player("Player 2", CellState.O, PlayerType.Human)
        };
    }

    public Player CurrentPlayer => _player[_currentPlayerIndex];    
    
    public Board Board => _board;
    
    public void StartGame(Player player1, Player player2)
    {
        _player = new Player[] { player1, player2 };
        _board.Reset();
        _currentPlayerIndex = 0;
    }

    public bool TryPlaceSymbol(int row, int col)
    {
        if (_board.IsEmpty(row, col))
        {
            _board.PlaceSymbol(row, col, CurrentPlayer.Symbol);
            return true;
        }

        return false;
    }
    
    public void SwitchPlayer()
    {
        _currentPlayerIndex = 1 - _currentPlayerIndex;
    }

    public CellState GetWinner()
    {
        return _checker.CheckWinner(_board);
    }

    public bool IsDraw()
    {
        return _checker.IsDraw(_board);
    }

}
