using MenuLib.Navigation;

namespace MenuLib.Core;

public class NavigateCommand : ICommand
{
    private NavigationManager _navigationManager;
    private IScreen _screen;
    
    public NavigateCommand(NavigationManager navigationManager, IScreen screen)
    {
        _navigationManager = navigationManager;
        _screen = screen;
    }


    public void Execute()
    {
        _navigationManager.GoTo(_screen);
    }
}