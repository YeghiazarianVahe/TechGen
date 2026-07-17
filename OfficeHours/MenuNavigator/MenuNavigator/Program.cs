using MenuNavigator.Services;
using MenuNavigator.Windows;

namespace MenuNavigator;

static class Program
{
    static void Main(string[] args)
    {
        NavigationController navigationController = new NavigationController();
        navigationController.AddWindow(new MainMenuWindow(navigationController));
        navigationController.AddWindow(new RegistrationAndExaminationWindow(navigationController));
        navigationController.AddWindow(new LoginSsnWindow(navigationController));
        navigationController.AddWindow(new IndividualPersonWindow(navigationController));
        navigationController.AddWindow(new AboutWindow(navigationController));
        InputController input = new InputController();
        
        navigationController.GoToWindow<MainMenuWindow>();
        
        MenuController controller = new MenuController(navigationController, input);
        
        controller.Start();
    }
}