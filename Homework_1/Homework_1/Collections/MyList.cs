namespace Homework_1.MyList;

public class MyList
{
    private int[] _items;
    private int _count = 0;
    private int _capacity = 4;

    public MyList()
    {
        _items = new int[_capacity];
    }

    public int Count
    {
        get { return _count; }
    }

    private void Resize()
    {
        if (_count == _capacity)
        {
            _capacity *= 2;
            int[] newItems = new int[_capacity];

            for (int i = 0; i < _count; i++)
            {
                newItems[i] = _items[i];
            }
            _items = newItems;
        }
    }

    public void Add(int item)
    {
        if (_count == _capacity)
        {
            Resize();
        }
        _items[_count] = item;
        _count++;
    }

    public void AddRange(int[] items)
    {
        if (items == null) return;
        foreach (int item in items)
        {
            Add(item);
        }
    }

    public bool Remove(int item)
    {
        int index = IndexOf(item);
        if (index == -1) return false;

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        
        _count--;
        return true;
    }

    public bool TryGet(int index, out int value)
    {
        if (index < 0 || index >= _count)
        {
            value = 0;
            return false;
        }

        value = _items[index];
        return true;
    }
    
    public int IndexOf(int item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_items[i] == item) return i;
        }
        return -1;
    }

    public bool Contains(int item)
    {
        return IndexOf(item) != -1;
    }

    public void Clear()
    {
        _count = 0;
    }

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            }

            return _items[index];
        }

        set
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            }

            _items[index] = value;
        }
    }

    public void Print()
    {
        for (int i = 0; i < Count; i++)
        {
            Console.Write(_items[i] + " ");
        }
        Console.WriteLine();
    }
}