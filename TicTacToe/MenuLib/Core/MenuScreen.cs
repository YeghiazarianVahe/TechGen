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
        _renderer = renderer;
        _navigation = manager;
        
        MenuItem[] temp =  new MenuItem[menuItems.Length + 2];
        for (int i = 0; i < menuItems.Length; i++)
        {
            temp[i] = menuItems[i];;
        }
        temp[menuItems.Length] = new MenuItem("[<- Backspace] Back", new GoBackCommand(_navigation));
        temp[menuItems.Length + 1] = new MenuItem("[ESC] Quit", new ExitCommand());
        _menuItems = temp;
    }

    public override void Render()
    {
        _renderer.Clear();
        _renderer.DrawText(Title);
        _renderer.DrawText("");
        
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
            if (_selectedIndex != 0)
                _selectedIndex--;
        }

        else if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S)
        {
            if (_selectedIndex != _menuItems.Length - 1)
                _selectedIndex++;
        }

        else if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
        {
            _menuItems[_selectedIndex].Command.Execute();
        }

        // Number keys: D1-D9
        else if (keyPressed.Key >= ConsoleKey.D1 && keyPressed.Key <= ConsoleKey.D9)
        {
            int index = keyPressed.Key - ConsoleKey.D1;

            if (index < _menuItems.Length)
            {
                _selectedIndex = index;
                _menuItems[_selectedIndex].Command.Execute();
            }
        }

        // Numpad keys: NumPad1-NumPad9
        else if (keyPressed.Key >= ConsoleKey.NumPad1 && keyPressed.Key <= ConsoleKey.NumPad9)
        {
            int index = keyPressed.Key - ConsoleKey.NumPad1;

            if (index < _menuItems.Length)
            {
                _selectedIndex = index;
                _menuItems[_selectedIndex].Command.Execute();
            }
        }

        else if (keyPressed.Key == ConsoleKey.Backspace)
        {
            _navigation.GoBack();
        }

        else if (keyPressed.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
    }
}