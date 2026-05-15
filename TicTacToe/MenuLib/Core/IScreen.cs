namespace MenuLib.Core;

public interface IScreen
{
    void Render();
    void HandleInput(ConsoleKeyInfo key);
}