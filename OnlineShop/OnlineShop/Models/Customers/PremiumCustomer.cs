namespace OnlineShop.Models.Customers;
using Orders;

public class PremiumCustomer : Customer
{
    public PremiumCustomer(string name, string email, Address address) : base(name, email, address, 10)
    {
    }
}