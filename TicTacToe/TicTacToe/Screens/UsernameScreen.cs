using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class UsernameScreen : Screen
{
   private readonly Renderer _renderer;
   private readonly NavigationManager _navigation;
   private readonly AppState _appState;
   private IScreen _nextScreen = null!;
   private bool _done = false; 
   
   public override bool HandlesOwnInput => true;
   
   
   public UsernameScreen(
       Renderer renderer,
       NavigationManager navigation,
       AppState appState)
   {
       _renderer = renderer;
       _navigation = navigation;
       _appState = appState;
   }

   public void SetNextScreen(IScreen nextScreen)
   {
       _nextScreen = nextScreen;
   }

   public override void Render()
   {
       if (_done) return;
       _renderer.Clear();
       _renderer.DrawHeader("Tic Tac Toe");
       _renderer.DrawSection("Welcome");
       _renderer.DrawMuted("Choose the name shown in the game screens.");
       _renderer.DrawText("");
       Console.Write("Enter your username: ");
       string? username = Console.ReadLine();
       if (string.IsNullOrEmpty(username))
       {
           username = "Player";
       }
       
       _appState.Username = username;
       _done = true;
       if (_nextScreen != null)
       {
           _navigation.Replace(_nextScreen);
       }
       
   }

   public override void HandleInput(ConsoleKeyInfo key)
   {
   }
}
