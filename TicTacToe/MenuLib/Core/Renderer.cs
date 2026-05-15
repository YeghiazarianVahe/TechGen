namespace MenuLib.Core;

public class Renderer
{
    public void Clear()
    {
        Console.Clear();
        Console.ResetColor();
    }

    public void DrawText(string text)
    {
        Console.WriteLine(text);
    }

    public void DrawAt(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine(text);
    }

    public void Highlight(string text)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(text);
        Console.ResetColor();
    }

}