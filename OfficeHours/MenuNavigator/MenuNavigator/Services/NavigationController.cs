using MenuNavigator.Models;
using MenuNavigator.Windows;

namespace MenuNavigator.Services;

public class NavigationController
{
    private readonly List<BaseWindow> _windows = new List<BaseWindow>();
    private BaseWindow? _currentWindow;
    private BaseWindow? _previousWindow;
    private BaseWindow? _nextWindow;
    public BaseWindow? CurrentWindow
    {
        get => _currentWindow;
        set => _currentWindow = value;
    }
    
    public BaseWindow? PreviousWindow
    {
        get => _previousWindow;
        set => _previousWindow = value;
    }
    public BaseWindow? NextWindow
    {
        get => _nextWindow;
        set => _nextWindow = value;
    }

   public void AddWindow(BaseWindow window)
    {
        _windows.Add(window);
    }

    public void GoToWindow<TWindow>() where TWindow : BaseWindow
    {
        foreach (BaseWindow window in _windows)
        {
            if (window.GetType() == typeof(TWindow))
            {
                PreviousWindow = CurrentWindow;
                CurrentWindow =  window;
                NextWindow = null;
                return;
            }
        }
        throw new Exception($"Window {typeof(TWindow).Name} not found");
    }

    public void GoToPreviousWindow()
    {
        if (PreviousWindow != null)
        {
            NextWindow = CurrentWindow;
            CurrentWindow = PreviousWindow;
            PreviousWindow = null;
        }
    }

    public void GoToNextWindow()
    {
        if (NextWindow != null)
        {
            PreviousWindow = CurrentWindow;
            CurrentWindow = NextWindow;
            NextWindow = null;
        }
    }

}