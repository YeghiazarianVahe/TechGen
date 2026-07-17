using MenuNavigator.Windows;

namespace MenuNavigator.Services;

public class MenuController
{
    private readonly NavigationController _navigator;
    private readonly InputController _inputController;

    public MenuController(NavigationController navigator, InputController inputController)
    {
        _navigator = navigator;
        _inputController = inputController;

        _inputController.MoveUp += () => GetCurrentWindow().Previous();
        _inputController.MoveDown += () => GetCurrentWindow().Next();
        _inputController.Select += () => GetCurrentWindow().Select();
        _inputController.Back += _navigator.GoToPreviousWindow;
        _inputController.Forward += _navigator.GoToNextWindow;
        _inputController.Quit += () => _inputController.Stop();
        
        _inputController.MoveUp += Refresh;
        _inputController.MoveDown += Refresh;
        _inputController.Select += Refresh;
        _inputController.Back += Refresh;
        _inputController.Forward += Refresh;
    }

    private void Refresh()
    {
        GetCurrentWindow().Render();
    }

    private BaseWindow GetCurrentWindow()
    {
        return _navigator.CurrentWindow ?? throw new Exception("Current  window is null");
    }

    public void Start()
    {
        _navigator.CurrentWindow?.Render();
        _inputController.Listener();
        CloseApp();
    }

    private void CloseApp()
    {
        Console.Clear();
        Console.WriteLine("Goodbye");
    }
}