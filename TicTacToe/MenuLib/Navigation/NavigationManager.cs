namespace MenuLib.Navigation;

using MenuLib.Core;

public class NavigationManager
{
    private IScreen _currentScreen = null!;
    private IScreen[] _history = new IScreen[10];
    private int _historyIndex = -1;

    public void GoTo(IScreen screen)
    {
        if (_historyIndex >= _history.Length - 1) return;
        _historyIndex++;
        _history[_historyIndex] = _currentScreen;
        _currentScreen = screen;
    }

    public void Replace(IScreen screen)
    {
        _currentScreen = screen;
    }

    public void ClearAndGoTo(IScreen screen)
    {
        _historyIndex = -1;
        for (int i = 0; i < _history.Length; i++)
        {
            _history[i] = null!;
        }

        _currentScreen = screen;
    }

    public void GoBack()
    {
        if (_historyIndex < 0) return;
        if (_history[_historyIndex] == null)
        {
            _historyIndex--;
            return;
        }
        _currentScreen = _history[_historyIndex];
        _historyIndex--;
    }

    public IScreen CurrentScreen => _currentScreen;
}
