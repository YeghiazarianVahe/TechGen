namespace OnlineShop.Models.Orders;
using Products;

public class OrderItem
{
    private int _quantity;

    public Product Product { get; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0) throw new ArgumentException("Quantity must be greater than 0.");
            _quantity = value;
        }
    }

    public OrderItem(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Quantity = quantity;
    }

    public decimal SubTotal => Product.Price * Quantity;

    public override string ToString()
    {
        return $"{Quantity} x {Product.Name} (${Product.Price:F2}) = ${SubTotal:F2}";
    }
}