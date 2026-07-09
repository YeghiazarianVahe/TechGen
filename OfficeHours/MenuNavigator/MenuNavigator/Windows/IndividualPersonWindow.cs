using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class IndividualPersonWindow : BaseWindow
{
    public IndividualPersonWindow(NavigationController navigate) : base("Individual Person", [
        new MenuItem("Login via MobileID", () => { }),
        new MenuItem("Login via SSN", navigate.GoToWindow<LoginSsnWindow>)
    ])
    {
    }
}