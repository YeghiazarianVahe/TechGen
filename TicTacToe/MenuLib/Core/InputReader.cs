namespace MenuLib.Core;

public class InputReader
{
    public ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey(true);
    }
}