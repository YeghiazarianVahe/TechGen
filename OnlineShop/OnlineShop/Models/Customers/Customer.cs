namespace OnlineShop.Models.Customers;
using Orders;

public class Customer
{
    public string Name { get; }
    public string Email { get; }
    public Address Address { get; }

    private readonly Order[] _orders;
    private int _orderCount;

    private readonly int _capacity;

    protected Customer(string name, string email, Address address, int capacity)
    {
     if(string.IsNullOrWhiteSpace(name)) throw  new ArgumentException("Name cannot be empty", nameof(name));
     if(string.IsNullOrWhiteSpace(email)) throw  new ArgumentException("Email cannot be empty", nameof(email));
     Address = address ?? throw new ArgumentNullException(nameof(address));
     Name = name;
     Email = email;
     _capacity = capacity;
     _orders = new Order[_capacity];
    }

    public Customer(string name, string email, Address address) : this(name, email, address, 5)
    {
    }

    public void PlaceOrder(Order order)
    {
        if(_orderCount >= _capacity) throw  new InvalidOperationException("Can't place more than " + _capacity + " orders");
        _orders[_orderCount] = order;
        _orderCount++;
    }

    public Order GetOrder(int index)
    {
        if (index < 0 || index >= _orderCount) throw new IndexOutOfRangeException();
        return _orders[index];
    }

    public int OrderCount => _orderCount;

    public override string ToString() => $"{Name} ({Email})";
}