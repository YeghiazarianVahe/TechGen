namespace MenuLib.Core;

public class MenuItem
{
    public string Label { get; }
    public ICommand Command { get; }

    public MenuItem(string label, ICommand command)
    {
        Label = label;
        Command = command;
    }
}