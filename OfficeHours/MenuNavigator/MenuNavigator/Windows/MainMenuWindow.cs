using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class MainMenuWindow : BaseWindow
{
    public MainMenuWindow(NavigationController navigate) : base("Main Menu", [
        new MenuItem("Registration and Examination Department", navigate.GoToWindow<RegistrationAndExaminationWindow>),
        new MenuItem("About RED", navigate.GoToWindow<AboutWindow>),
        new MenuItem("List of Questions", () => { })
    ])
    {
    }
}