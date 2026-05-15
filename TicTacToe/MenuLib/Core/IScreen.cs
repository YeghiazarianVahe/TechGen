namespace MenuLib.Core;

public interface IScreen
{
    bool HandlesOwnInput { get; }
    void Render();
    void HandleInput(ConsoleKeyInfo key);
}