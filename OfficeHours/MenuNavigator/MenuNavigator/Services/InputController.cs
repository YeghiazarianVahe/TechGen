namespace MenuNavigator.Services;

public class InputController
{
    public event Action? MoveUp;
    public event Action? MoveDown;
    public event Action? Select;
    public event Action? Back;
    public event Action? Forward;
    public event Action? Quit;

    private bool _isRunning = true;

    public void Listener()
    {
        while (_isRunning)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp?.Invoke();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown?.Invoke();
                    break;
                case ConsoleKey.LeftArrow:
                    Back?.Invoke();
                    break;
                case ConsoleKey.RightArrow:
                    Forward?.Invoke();
                    break;
                case ConsoleKey.Enter:
                    Select?.Invoke();
                    break;
                case ConsoleKey.Escape:
                    Quit?.Invoke();
                    break;
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
    }
}