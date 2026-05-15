namespace MenuLib.Core;

public class Renderer
{
    private const int Width = 56;

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
        Console.Write(text);
    }

    public void Highlight(string text)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(Pad(text));
        Console.ResetColor();
        Console.WriteLine();
    }

    public void DrawHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("+" + Repeat("-", Width - 2) + "+");
        Console.WriteLine("|" + Center(title, Width - 2) + "|");
        Console.WriteLine("+" + Repeat("-", Width - 2) + "+");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void DrawSection(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public void DrawMuted(string text)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public void DrawLabelValue(string label, string value)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(label);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(value);
        Console.ResetColor();
    }

    public void DrawFooter(string text)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Repeat("-", Width));
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private string Pad(string text)
    {
        if (text.Length >= Width) return text;
        return text + Repeat(" ", Width - text.Length);
    }

    private string Center(string text, int width)
    {
        if (text.Length >= width) return text;
        int left = (width - text.Length) / 2;
        int right = width - text.Length - left;
        return Repeat(" ", left) + text + Repeat(" ", right);
    }

    private string Repeat(string text, int count)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result += text;
        }

        return result;
    }
}
