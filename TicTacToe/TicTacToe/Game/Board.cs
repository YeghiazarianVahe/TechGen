namespace TicTacToe.Game;

public class Board
{
    private Cell[,] _cells;

    public Board()
    {
        _cells = new Cell[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _cells[i, j] = new Cell();
            }
        }
    }

    public bool IsEmpty(int row, int col)
    {
        if (!IsInside(row, col)) return false;
        return _cells[row, col].IsEmpty;
    }

    public void PlaceSymbol(int row, int col, CellState symbol)
    {
        if (!IsInside(row, col)) return;
        _cells[row, col].State = symbol;
    }

    public CellState GetCell(int row, int col)
    {
        if (!IsInside(row, col)) return CellState.Empty;
        return _cells[row, col].State;
    }

    public void Reset()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                PlaceSymbol(i, j, CellState.Empty);
            }
        }
    }

    private bool IsInside(int row, int col)
    {
        return row >= 0 && row < 3 && col >= 0 && col < 3;
    }
}
