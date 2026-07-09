using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class LoginSsnWindow : BaseWindow
{
    public LoginSsnWindow(NavigationController navigate) : base("Login via SSN", [
        new MenuItem("Ender detail information", () => { }),
        new MenuItem("Show my current registrations", () => { })
    ])
    {
    }
}