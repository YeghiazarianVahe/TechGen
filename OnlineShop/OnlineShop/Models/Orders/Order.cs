namespace OnlineShop.Models.Orders;

public class Order
{
    private readonly OrderItem[] _orderItems;
    private int _itemCount;

    public int Id { get; }
    public DateTime PlacedAt { get; }

    public Order(int id, int capacity)
    {
        if (id <= 0) throw new ArgumentException("Id must be greater than zero.", nameof(id));
        Id = id;
        if (capacity <= 0) throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));        
        _orderItems = new OrderItem[capacity];
        PlacedAt = DateTime.Now;
    }

    public void AddItem(OrderItem item)
    {
        if (_itemCount >= _orderItems.Length) throw new InvalidOperationException("Can't place more than " + _orderItems.Length + " items");
        _orderItems[_itemCount] = item;
        _itemCount++;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            for (int i = 0; i < _itemCount; i++)
                totalPrice += _orderItems[i].SubTotal;
            return totalPrice;
        }
    }

    public OrderItem GetItem(int index)
    {
        if (index < 0 || index >= _itemCount) throw new ArgumentOutOfRangeException(nameof(index));
        return _orderItems[index];
    }

    public int ItemCount => _itemCount;
    
    public override string ToString()
    {
        return $"Order #{Id} | {PlacedAt:yyyy MMMM dd} |  {_itemCount} item(s) | Total: ${TotalPrice:F2}";
    }
}