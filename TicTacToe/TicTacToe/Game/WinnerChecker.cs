namespace TicTacToe.Game;

public class WinnerChecker
{
    private bool CheckLine(
        Board board,
        int r1, int c1,
        int r2, int c2,
        int r3, int c3)
    {
        CellState first = board.GetCell(r1, c1);
        return first != CellState.Empty &&
               first == board.GetCell(r2, c2) &&
               first == board.GetCell(r3, c3);
    }
    
    public CellState CheckWinner(Board board)
    {
        // Horizontal lines
        if (CheckLine(board, 0, 0, 0, 1, 0, 2)) return board.GetCell(0, 0);
        if (CheckLine(board, 1, 0, 1, 1, 1, 2)) return board.GetCell(1, 0);
        if (CheckLine(board, 2, 0, 2, 1, 2, 2)) return board.GetCell(2, 0);
        
        // Vertical lines
        if (CheckLine(board, 0, 0, 1, 0, 2, 0)) return board.GetCell(0, 0);
        if (CheckLine(board, 0, 1, 1, 1, 2, 1)) return board.GetCell(0, 1);
        if (CheckLine(board, 0, 2, 1, 2, 2, 2)) return board.GetCell(0, 2);

        // Diagonal lines
        if (CheckLine(board, 0, 0, 1, 1, 2, 2)) return board.GetCell(0, 0);
        if (CheckLine(board, 0, 2, 1, 1, 2, 0)) return board.GetCell(0, 2);

        return CellState.Empty;
    }

    public bool IsDraw(Board board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board.GetCell(i, j) == CellState.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }

}