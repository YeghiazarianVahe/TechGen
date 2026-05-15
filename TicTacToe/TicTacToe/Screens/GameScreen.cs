using MenuLib.Core;
using MenuLib.Navigation;
using TicTacToe.Game;

namespace TicTacToe.Screens;

public class GameScreen : Screen
{
    private readonly AppState _appState;
    private readonly NavigationManager _navigation;
    private readonly Renderer _renderer;
    private IScreen _mainMenu = null!;
    private int _selectedRow;
    private int _selectedColumn;
    private bool _gameOver;
    private string _message = "";

    public GameScreen(AppState appState, NavigationManager navigation, Renderer renderer)
    {
        _appState = appState;
        _navigation = navigation;
        _renderer = renderer;
    }

    public void SetMainMenu(IScreen mainMenu)
    {
        _mainMenu = mainMenu;
    }

    public void ResetState()
    {
        _selectedRow = 0;
        _selectedColumn = 0;
        _gameOver = false;
        _message = "";
    }

    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawHeader("Tic Tac Toe");
        _renderer.DrawLabelValue("Mode: ", GetModeText());
        _renderer.DrawLabelValue("Turn: ", GetTurnText());
        _renderer.DrawText("");
        DrawBoard();
        _renderer.DrawText("");

        if (!string.IsNullOrEmpty(_message))
        {
            _renderer.DrawSection(_message);
        }

        if (_gameOver)
        {
            _renderer.DrawFooter("[Enter] Main Menu   [Backspace] Main Menu   [ESC] Quit");
        }
        else
        {
            _renderer.DrawFooter("Move: Arrows/WASD   [Enter] Place   [Backspace] Back   [ESC] Quit");
        }
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        if (_gameOver)
        {
            HandleGameOverInput(key);
            return;
        }

        if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
        {
            if (_selectedRow > 0) _selectedRow--;
        }
        else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
        {
            if (_selectedRow < 2) _selectedRow++;
        }
        else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
        {
            if (_selectedColumn > 0) _selectedColumn--;
        }
        else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
        {
            if (_selectedColumn < 2) _selectedColumn++;
        }
        else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
        {
            PlaceCurrentPlayerSymbol();
        }
        else if (key.Key == ConsoleKey.Backspace)
        {
            _navigation.GoBack();
        }
        else if (key.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
    }

    private void HandleGameOverInput(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Backspace)
        {
            _gameOver = false;
            _message = "";
            if (_mainMenu != null)
            {
                _navigation.ClearAndGoTo(_mainMenu);
            }
        }
        else if (key.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
    }

    private void PlaceCurrentPlayerSymbol()
    {
        if (!_appState.Engine.TryPlaceSymbol(_selectedRow, _selectedColumn))
        {
            _message = "This cell is already taken.";
            return;
        }

        if (UpdateGameResult()) return;

        _appState.Engine.SwitchPlayer();
        _message = "";
    }

    private bool UpdateGameResult()
    {
        CellState winner = _appState.Engine.GetWinner();
        if (winner != CellState.Empty)
        {
            _gameOver = true;
            _message = GetWinnerName(winner) + " wins.";
            return true;
        }

        if (_appState.Engine.IsDraw())
        {
            _gameOver = true;
            _message = "Draw.";
            return true;
        }

        return false;
    }

    private void DrawBoard()
    {
        for (int row = 0; row < 3; row++)
        {
            DrawBoardRow(row);

            if (row < 2)
            {
                Console.WriteLine("       ---+---+---");
            }
        }
    }

    private void DrawBoardRow(int row)
    {
        Console.Write("        ");
        for (int column = 0; column < 3; column++)
        {
            DrawCell(row, column);

            if (column < 2)
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();
    }

    private void DrawCell(int row, int column)
    {
        string symbol = GetCellText(_appState.Engine.Board.GetCell(row, column));

        if (row == _selectedRow && column == _selectedColumn && !_gameOver)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" " + symbol + " ");
            Console.ResetColor();
            return;
        }

        Console.Write(" " + symbol + " ");
    }

    private string GetCellText(CellState cell)
    {
        if (cell == CellState.X) return "X";
        if (cell == CellState.O) return "O";
        return " ";
    }

    private string GetModeText()
    {
        return "Player vs Player";
    }

    private string GetTurnText()
    {
        if (_gameOver) return "-";
        return _appState.Engine.CurrentPlayer.Name + " (" + GetCellText(_appState.Engine.CurrentPlayer.Symbol) + ")";
    }

    private string GetWinnerName(CellState winner)
    {
        if (_appState.Engine.CurrentPlayer.Symbol == winner)
        {
            return _appState.Engine.CurrentPlayer.Name;
        }

        if (winner == _appState.PlayerSymbol)
        {
            return _appState.Username;
        }

        return "Player 2";
    }
}
