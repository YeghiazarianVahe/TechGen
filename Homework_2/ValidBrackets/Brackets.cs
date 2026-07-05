namespace Homework2;

public class CharStack
{
    private char[] _items;
    private int _count;

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public CharStack(int capacity = 4)
    {
        if (capacity <= 0)
            capacity = 4;

        _items = new char[capacity];
    }

    public void Push(char c)
    {
        if (_count == _items.Length)
        {
            Resize();
        }

        _items[_count] = c;
        _count++;
    }

    public char Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Stack is empty");

        _count--;
        return _items[_count];
    }

    public char Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Stack is empty");

        return _items[_count - 1];
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        char[] newItems = new char[newCapacity];

        for (int i = 0; i < _count; i++)
        {
            newItems[i] = _items[i];
        }

        _items = newItems;
    }

    public static bool IsValid(string input)
    {
        CharStack stack = new CharStack();

        foreach (char c in input)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.IsEmpty)
                    return false;

                char top = stack.Pop();

                if ((c == ')' && top != '(') ||
                    (c == ']' && top != '[') ||
                    (c == '}' && top != '{'))
                {
                    return false;
                }
            }
        }

        return stack.IsEmpty;
    }


    public static void TestBrackets()
    {
        Console.WriteLine(CharStack.IsValid("()"));         
        Console.WriteLine(CharStack.IsValid("[]"));          
        Console.WriteLine(CharStack.IsValid("{}"));          
        Console.WriteLine(CharStack.IsValid("{[()]}"));      
        Console.WriteLine(CharStack.IsValid("{[(])}"));      
        Console.WriteLine(CharStack.IsValid("([)]"));        
        Console.WriteLine(CharStack.IsValid("((("));        
        Console.WriteLine(CharStack.IsValid("})"));         
    }
}