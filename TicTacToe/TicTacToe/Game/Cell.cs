namespace TicTacToe.Game;

public class Cell
{
    private CellState _state;

    public CellState State
    {
        get => _state;
        set => _state = value;
    }

    public Cell()
    {
        _state = CellState.Empty;
    }
    
    public bool  IsEmpty => _state == CellState.Empty;
}