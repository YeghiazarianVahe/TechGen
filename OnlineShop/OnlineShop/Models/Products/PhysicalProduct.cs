namespace OnlineShop.Models.Products;
using Interfaces;

public class PhysicalProduct : Product,  IShippable
{
    private decimal _weight;

    public decimal Weight
    {
        get => _weight;
        set
        {
            if(value <= 0) throw new ArgumentException("Weight must be greater than zero");
            _weight = value;
        }
    }

    public PhysicalProduct(string name, decimal price, decimal weight) : base(name, price)
    {
        Weight = weight;
    }

    public override string GetDescription()
    {
        return $"Physical product {Name}, Weight: {Weight}kg";
    }

    public decimal CalculateShipping()
    {
        return Weight * 0.5m;
    }
}