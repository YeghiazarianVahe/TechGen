namespace MenuLib.Core;

public abstract class Screen : IScreen
{
    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
        }
    }

    public virtual bool HandlesOwnInput => false;
    public abstract void Render();

    public abstract void HandleInput(ConsoleKeyInfo key);
}