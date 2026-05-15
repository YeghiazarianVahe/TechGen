using MenuLib.Core;
using MenuLib.Navigation;
using TicTacToe.Screens;

namespace TicTacToe;

public class Program
{
    public static void Main(string[] args)
    {
        Renderer renderer = new Renderer();
        InputReader inputReader = new InputReader();
        NavigationManager navigation = new NavigationManager();
        AppState appState = new AppState();

        AboutScreen aboutScreen = new AboutScreen(renderer, navigation, appState);
        SettingsScreen settingsScreen = new SettingsScreen(appState, navigation, renderer);
        
        MenuItem[] mainMenuItems = new MenuItem[]
        {
            new MenuItem("Play",     new NavigateCommand(navigation, aboutScreen)),
            new MenuItem("Settings", new NavigateCommand(navigation, settingsScreen)),
            new MenuItem("About",    new NavigateCommand(navigation, aboutScreen)),
            new MenuItem("Quit",     new ExitCommand())
        };

        MenuScreen mainMenu = new MenuScreen("Main Menu", mainMenuItems, renderer, navigation);
        
        UsernameScreen usernameScreen = new UsernameScreen(renderer, navigation, appState);
        usernameScreen.SetNextScreen(mainMenu);
        
        navigation.GoTo(usernameScreen);

        while (true)
        {
            navigation.CurrentScreen.Render();
            if (!navigation.CurrentScreen.HandlesOwnInput)
            {
                ConsoleKeyInfo key = inputReader.ReadKey();
                navigation.CurrentScreen.HandleInput(key);
            }
        }
    }
}