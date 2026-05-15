using MenuLib.Navigation;

namespace MenuLib.Core;

public class MenuScreen : Screen
{
    private MenuItem[] _menuItems;
    private int _selectedIndex;
    private Renderer _renderer;
    private NavigationManager _navigation;

    public MenuScreen(string title, MenuItem[] menuItems, Renderer renderer, NavigationManager manager)
    {
        Title = title;
        _menuItems = menuItems;
        _renderer = renderer;
        _navigation = manager;
    }

    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawText(Title);

        for (int i = 0; i < _menuItems.Length; i++)
        {
            if (i == _selectedIndex)
                _renderer.Highlight(_menuItems[i].Label);
            else
                _renderer.DrawText(_menuItems[i].Label);
        }
    }

    public override void HandleInput(ConsoleKeyInfo keyPressed)
    {
        if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W)
        {
            if (_selectedIndex != 0) _selectedIndex--;
        }

        if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S)
        {
            if (_selectedIndex != _menuItems.Length - 1) _selectedIndex++;
        }

        if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
        {
            _menuItems[_selectedIndex].Command.Execute();
        }

        if (keyPressed.Key == ConsoleKey.Backspace)
        {
            _navigation.GoBack();
        }
        
    }
}