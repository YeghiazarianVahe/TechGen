using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class RegistrationAndExaminationWindow : BaseWindow
{
    public RegistrationAndExaminationWindow(NavigationController navigate) : base("Registration and Examination Department", [
        new MenuItem("List of point for registration and examination.", () => { }),
        new MenuItem("Individual person", navigate.GoToWindow<IndividualPersonWindow>),
        new MenuItem("Business person", () => { })
    ])
    {
    }
}