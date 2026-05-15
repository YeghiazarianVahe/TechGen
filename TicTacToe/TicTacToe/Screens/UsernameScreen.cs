using MenuLib.Core;
using MenuLib.Navigation;

namespace TicTacToe.Screens;

public class UsernameScreen : Screen
{
   private readonly Renderer _renderer;
   private readonly NavigationManager _navigation;
   private readonly AppState _appState;
   private IScreen _nextScreen;
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
       Console.Clear();
       _renderer.DrawText("Tic Tac Toe");
       Console.Write("Enter your username: ");
       string?  username = Console.ReadLine();
       if (string.IsNullOrEmpty(username))
       {
           username = "Player";
       }
       
       _appState.Username = username;
       _done = true;
       _navigation.GoTo(_nextScreen);
       
   }

   public override void HandleInput(ConsoleKeyInfo key)
   {
       // Not used — input handled in Render via Console.ReadLine
   }
}