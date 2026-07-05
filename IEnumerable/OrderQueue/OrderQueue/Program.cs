using System.Collections;

namespace OrderQueue;

class Order
{
    public int Id { get; }
    public string CustomerName { get; }
    public decimal Total { get; }

    public Order(int id, string customerName, decimal total)
    {
        Id = id;
        CustomerName = customerName;
        if (total < 0) throw new ArgumentOutOfRangeException(nameof(total));
        Total = total;
    }
}

public class OrderQueueFullException : Exception
{
    public  OrderQueueFullException(string message) : base(message)
    {
    }
}

public class OrderQueueEmptyException : Exception
{
    public OrderQueueEmptyException(string message) : base(message)
    {
    }
}

class OrderQueue : IEnumerable
{
    private Order[] _orders;
    private int _count;
    private int _capacity;
  
    public OrderQueue(int capacity = 4)
    {
        _capacity = capacity;
        _orders = new Order[_capacity];    
    }

    public void Enqueue(Order order)
    {
        if (_count >= _capacity) throw new OrderQueueFullException("Order Queue is full.");
        _orders[_count] = order;
        _count++;
    }

    public Order Dequeue()
    {
        if (_count == 0) throw new OrderQueueEmptyException("Order Queue is empty.");
        Order item = _orders[0];
        for (int i = 0; i < _count - 1; i++)
        {
            _orders[i] = _orders[i + 1];
        }

        _count--;
        return item;
    }

    public int Count => _count;

    public bool IsEmpty => Count == 0;
    public bool IsFull => Count == _orders.Length;
    
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _orders[i];
        }    
    }
}


class Program
{
    static void Main(string[] args)
    {
        OrderQueue queue = new OrderQueue(3);

        try
        {
            queue.Enqueue(new Order(1, "Vahe", 12000));
            queue.Enqueue(new Order(2, "Ani", 8500));
            queue.Enqueue(new Order(3, "Aram", 4300));
        }
        catch (OrderQueueFullException ex)
        {
            Console.WriteLine($"Cannot enqueue order: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Enqueue attempt finished");
        }

        Console.WriteLine();
        Console.WriteLine("Current orders in queue:");

        foreach (Order order in queue)
        {
            Console.WriteLine($"Id: {order.Id}, Customer: {order.CustomerName}, Total: {order.Total}");
        }

        Console.WriteLine();
        Console.WriteLine("Dequeuing orders:");

        try
        {
            while (!queue.IsEmpty)
            {
                Order order = queue.Dequeue();
                Console.WriteLine($"Dequeued: Id: {order.Id}, Customer: {order.CustomerName}, Total: {order.Total}");
            }

            queue.Dequeue();
        }
        catch (OrderQueueEmptyException ex)
        {
            Console.WriteLine($"Cannot dequeue order: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Dequeue attempt finished");
        }
    }
}