using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class AboutWindow : BaseWindow
{
    public AboutWindow(NavigationController navigate) : base("About RED", [
        new MenuItem("Out Staff", () => { }),
        new MenuItem("About us", () => { })
    ])
    {
    }
}