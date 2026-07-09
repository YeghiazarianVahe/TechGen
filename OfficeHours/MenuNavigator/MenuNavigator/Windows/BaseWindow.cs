using MenuNavigator.Interfaces;
using MenuNavigator.Models;

namespace MenuNavigator.Windows;

public abstract class BaseWindow : IRenderable
{
    private string _title;
    private List<MenuItem> _menuItems;
    private int _selectedIndex;
    
    protected string Title { get; }
    protected List<MenuItem> MenuItems { get; }
    protected int SelectedIndex { get; set; }

    protected BaseWindow(string title, List<MenuItem> menuItems)
    {
        Title = title;
        MenuItems = menuItems;
    }

    public void Previous()
    {
        if (SelectedIndex > 0)
        {
            SelectedIndex--;
        }
        else
        {
            SelectedIndex = MenuItems.Count - 1;
        }
    }

    public void Next()
    {
        if (SelectedIndex < MenuItems.Count - 1)
        {
            SelectedIndex++;
        }
        else
        {
            SelectedIndex = 0;
        }
    }

    public virtual void Render()
    {
        PrintHeader();
        for (int i = 0; i < MenuItems.Count; i++)
        {
            if (SelectedIndex == i)
            {
                Console.Write(" > ");
            }
            Console.WriteLine(MenuItems[i].Title);
        }
        PrintControls();
    }

    private void PrintHeader()
    {
        Console.WriteLine("************************************");
        Console.WriteLine($"***** {Title} *****");
        Console.WriteLine("************************************");
        Console.WriteLine("");
    }

    private void PrintControls()
    {
        Console.WriteLine();
        Console.WriteLine("Use up/down keys for navigation. ");
        Console.WriteLine("Press Enter to select option.");
        Console.WriteLine("Press Backspace to go back.");
        Console.WriteLine("Press Esc to quit.");
    }

    public void Select()
    {
        MenuItems[SelectedIndex].Action.Invoke();
    }
}