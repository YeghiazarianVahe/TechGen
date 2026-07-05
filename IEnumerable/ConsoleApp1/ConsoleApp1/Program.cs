using System.Collections;

namespace MyEnumerator;


class EvenRange : IMyEnumerable
{
    private readonly int _min;
    private readonly int _max;

    public EvenRange(int min, int max)
    {
        if(min >= max) throw new  ArgumentOutOfRangeException(nameof(min));
        _min = min;
        _max = max;
    }

    public IMyEnumerator GetEnumerator() => new EvenRangeEnumerator(_min, _max);

    private class EvenRangeEnumerator : IMyEnumerator
    {
        private readonly int _min;
        private readonly int _max;
        private int current;
        
        public EvenRangeEnumerator(int min, int max)
        {
            _min = min;
            _max = max;
            current = _min - 1;
        }

        public int Current => current;

        public bool MoveNext()
        {
            while (current <= _max && current % 2 == 0)
                current++;
            current++;
            return  current <= _max;
        }

        public void Reset()
        {
            current = _min - 1;
        }
    }

}


class Program
{
    static void Main(string[] args)
    {
        EvenRange sequence = new EvenRange(1, 11);
        IMyEnumerator enumerator = sequence.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
    }
}