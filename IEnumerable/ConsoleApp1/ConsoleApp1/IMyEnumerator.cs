namespace MyEnumerator;

public interface IMyEnumerator
{
    bool MoveNext();
    int Current { get; }
    void Reset();
}