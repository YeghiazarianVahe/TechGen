namespace MenuLib.Navigation;

using MenuLib.Core;

public class NavigationManager
{
    private IScreen _currentScreen;
    private IScreen[] _history = new IScreen[10];
    private int _historyIndex = -1;

    public void GoTo(IScreen screen)
    {
        if (_historyIndex >= _history.Length - 1) return;
        _historyIndex++;
        _history[_historyIndex] = _currentScreen;
        _currentScreen = screen;
    }

    public void GoBack()
    {
        if (_historyIndex < 0) return;
        _currentScreen = _history[_historyIndex];
        _historyIndex--;
    }

    public IScreen CurrentScreen => _currentScreen;
}