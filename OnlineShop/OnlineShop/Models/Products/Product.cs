namespace OnlineShop.Models.Products;

public abstract class Product
{
    public string Name { get; }
   
    private decimal _price;

    public decimal Price
    {
        get => _price;

        set
        {
            if (value <= 0) throw new ArgumentException("Price must be greater than zero");
            _price = value;
        }
    }

    protected Product(string name, decimal price)
    {
        if(string.IsNullOrEmpty(name)) throw new ArgumentException("Product name cannot be null or empty");
        Name = name;
        Price = price;
    }

    public abstract string GetDescription();

    public override string ToString() => $"{Name} (${Price:F2})";
}