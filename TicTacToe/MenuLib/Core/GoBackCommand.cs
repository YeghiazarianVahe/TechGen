using MenuLib.Navigation;

namespace MenuLib.Core;

public class GoBackCommand : ICommand
{
    private NavigationManager _navigation;
    public GoBackCommand(NavigationManager manager)
    {
        _navigation = manager;
    }

    public void Execute()
    {
        _navigation.GoBack();
    }
}