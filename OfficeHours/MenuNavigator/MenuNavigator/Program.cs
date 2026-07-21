using MenuNavigator.Interfaces;
using MenuNavigator.Models;
using MenuNavigator.Services;
using MenuNavigator.Windows;

namespace MenuNavigator;

static class Program
{
    static void Main(string[] args)
    {
        IFileSaver saver = new TextFileSaver();
        NavigationController navigationController = new NavigationController();
        navigationController.AddWindow(new MainMenuWindow(navigationController));
        navigationController.AddWindow(new RegistrationAndExaminationWindow(navigationController));
        navigationController.AddWindow(new LoginSsnWindow(navigationController));
        navigationController.AddWindow(new IndividualPersonWindow(navigationController));
        navigationController.AddWindow(new AboutWindow(navigationController));
        navigationController.AddWindow(new CalendarWindow(navigationController, saver));
        InputController input = new InputController();
        
        navigationController.GoToWindow<MainMenuWindow>();
        
        MenuController controller = new MenuController(navigationController, input);
        
        controller.Start();
        
    }
}