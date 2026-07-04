namespace OnlineShop.Models.Cart;
using Products;
using Orders;

public class ShoppingCart
{
    private readonly OrderItem[] _items;
    private int _itemCount;

    public ShoppingCart(int capacity)
    {
        if (capacity <= 0) throw new ArgumentException("Invalid capacity");
        _items = new OrderItem[capacity];
    }

    public void AddItem(Product product, int quantity)
    {
        if (_itemCount >= _items.Length) throw new InvalidOperationException("Cart is full.");
        _items[_itemCount++] = new(product, quantity);
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= _itemCount) throw new ArgumentOutOfRangeException(nameof(index));
        for (int i = index; i < _itemCount - 1; i++)
            _items[i] = _items[i + 1];
        _items[_itemCount - 1] = null;  
        _itemCount--;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            for(int i = 0; i < _itemCount; i++)
                totalPrice += _items[i].SubTotal;
            return totalPrice;
        }
    }   
    
    public int ItemCount => _itemCount;

    public Order Checkout(int orderId)
    {
        if (_itemCount == 0) throw new InvalidOperationException("Cannot checkout an empty cart");
        Order order = new Order(orderId, _itemCount);
        for (int i = 0; i < _itemCount; i++)
            order.AddItem(_items[i]);
        Clear();
        return order;
    }

    private void Clear()
    {
        for (int i = 0; i < _itemCount; i++)
            _items[i] = null;
        _itemCount = 0;    
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Shopping Cart ({_itemCount} item(s)):");

        for (int i = 0; i < _itemCount; i++)
            sb.AppendLine($"  {_items[i]}");   // reuses OrderItem.ToString()

        sb.Append($"Total: ${TotalPrice:F2}");
        return sb.ToString();
    }
}