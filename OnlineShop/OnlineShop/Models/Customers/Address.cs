namespace OnlineShop.Models.Customers;

public class Address
{
    private readonly string _address;
    
    public Address(string address)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException(nameof(address));
        _address = address;
    }
    
    public override string ToString() => _address;
}