using MenuNavigator.Windows;

namespace MenuNavigator.Services;

public class MenuController
{
    private readonly NavigationController _navigator;
    private bool _isRunning = true;

    public MenuController(NavigationController navigator)
    {
        _navigator = navigator;
    }


    public void Run()
    {
        while (_isRunning)
        {
            BaseWindow currentWindow = GetCurrentWindow();
            currentWindow.Render();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            HandleInput(keyInfo, currentWindow);
        }

        CloseApp();
    }

    private void CloseApp()
    {
        Console.Clear();
        Console.WriteLine("Good bye! :)");
    }

    private void HandleInput(ConsoleKeyInfo keyInfo, BaseWindow currentWindow)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                currentWindow.Previous();
                break;
            case ConsoleKey.DownArrow:
                currentWindow.Next();
                break;
            case ConsoleKey.Enter:
                currentWindow.Select();
                break;
            case ConsoleKey.LeftArrow:
                _navigator.GoToPreviousWindow();
                break;
            case ConsoleKey.RightArrow:
                _navigator.GoToNextWindow();
                break;
            case ConsoleKey.Q:
                _isRunning = false;
                break;
        }
    }

    private BaseWindow GetCurrentWindow()
    {
        if (_navigator.CurrentWindow == null)
        {
            throw new Exception("Current  window is null");
        }
        return _navigator.CurrentWindow;
    }
}