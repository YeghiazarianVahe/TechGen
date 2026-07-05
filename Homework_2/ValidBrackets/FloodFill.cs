using System;

namespace Homework2;

public readonly struct Cell
{
    public int X { get; }
    public int Y { get; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class CellStack
{
    private Cell[] _cells;
    private int _count;

    public CellStack(int initialCapacity = 4)
    {
        if (initialCapacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialCapacity));

        _cells = new Cell[initialCapacity];
    }

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public void Push(Cell cell)
    {
        if (_count == _cells.Length)
            Grow(_cells.Length * 2);

        _cells[_count++] = cell;
    }

    public Cell Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Stack is empty.");

        _count--;
        return _cells[_count];
    }

    private void Grow(int newCapacity)
    {
        Cell[] newCells = new Cell[newCapacity];

        for (int i = 0; i < _count; i++)
            newCells[i] = _cells[i];

        _cells = newCells;
    }
}

public static class FloodFillAlgorithm
{
    public static void FloodFill(int[,] grid, int startX, int startY, int newValue)
    {
        ArgumentNullException.ThrowIfNull(grid);

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        if (!IsInside(startX, startY, rows, cols))
            throw new ArgumentOutOfRangeException("Start cell is outside the grid.");

        int originalValue = grid[startX, startY];

        if (originalValue == newValue)
            return;

        CellStack stack = new CellStack();
        stack.Push(new Cell(startX, startY));

        while (!stack.IsEmpty)
        {
            Cell current = stack.Pop();

            if (!IsInside(current.X, current.Y, rows, cols))
                continue;

            if (grid[current.X, current.Y] != originalValue)
                continue;

            grid[current.X, current.Y] = newValue;

            stack.Push(new Cell(current.X - 1, current.Y));
            stack.Push(new Cell(current.X + 1, current.Y));
            stack.Push(new Cell(current.X, current.Y - 1));
            stack.Push(new Cell(current.X, current.Y + 1));
        }
    }

    public static void FloodFillRecursive(int[,] grid, int startX, int startY, int newValue)
    {
        ArgumentNullException.ThrowIfNull(grid);

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        if (!IsInside(startX, startY, rows, cols))
            throw new ArgumentOutOfRangeException("Start cell is outside the grid.");

        int originalValue = grid[startX, startY];

        if (originalValue == newValue)
            return;

        Fill(grid, startX, startY, originalValue, newValue);
    }

    private static void Fill(int[,] grid, int x, int y, int originalValue, int newValue)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        if (!IsInside(x, y, rows, cols))
            return;

        if (grid[x, y] != originalValue)
            return;

        grid[x, y] = newValue;

        Fill(grid, x - 1, y, originalValue, newValue);
        Fill(grid, x + 1, y, originalValue, newValue);
        Fill(grid, x, y - 1, originalValue, newValue);
        Fill(grid, x, y + 1, originalValue, newValue);
    }

    public static void TestFloodFill()
    {
        int[,] grid =
        {
            { 1, 1, 1, 0, 0, 2, 2, 2 },
            { 1, 5, 5, 0, 0, 2, 3, 3 },
            { 1, 5, 5, 5, 0, 2, 3, 3 },
            { 0, 5, 5, 5, 0, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 4, 4, 4 },
            { 7, 7, 0, 8, 8, 4, 6, 6 },
            { 7, 7, 0, 8, 8, 4, 6, 6 },
            { 7, 7, 0, 0, 0, 4, 6, 6 }
        };

        int[,] expected =
        {
            { 1, 1, 1, 0, 0, 2, 2, 2 },
            { 1, 9, 9, 0, 0, 2, 3, 3 },
            { 1, 9, 9, 9, 0, 2, 3, 3 },
            { 0, 9, 9, 9, 0, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 4, 4, 4 },
            { 7, 7, 0, 8, 8, 4, 6, 6 },
            { 7, 7, 0, 8, 8, 4, 6, 6 },
            { 7, 7, 0, 0, 0, 4, 6, 6 }
        };

        FloodFillRecursive(grid, 2, 2, 9);

        PrintGrid(grid);
        Console.WriteLine(GridsEqual(grid, expected) ? "PASS" : "FAILED");
    }

    private static bool IsInside(int x, int y, int rows, int cols)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols;
    }

    private static void PrintGrid(int[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
                Console.Write(grid[i, j] + " ");

            Console.WriteLine();
        }
    }

    private static bool GridsEqual(int[,] a, int[,] b)
    {
        if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
            return false;

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if (a[i, j] != b[i, j])
                    return false;
            }
        }

        return true;
    }
}