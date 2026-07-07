namespace MiniBill;

class Product
{
    public string Name { get; }
    public decimal Price { get; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class Menu
{
    private readonly List<Product> _products = new()
    {
        new Product("Burger", 8),
        new Product("Pizza", 12),
        new Product("Drink", 3)
    };

    public void PrintMenu()
    {
        Console.WriteLine("Menu:");
        for (int i = 0; i < _products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_products[i].Name} - ${_products[i].Price}");
        }
    }

    public decimal CalculateBill(List<int> quantities)
    {
        decimal total = 0;

        for (int i = 0; i < _products.Count; i++)
        {
            total += quantities[i] * _products[i].Price;
        }

        return total;
    }

    public bool HasFreeDessert(decimal totalPrice)
    {
        return totalPrice > 30;
    }

    public int ProductCount => _products.Count;
}

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        menu.PrintMenu();

        Console.WriteLine();
        Console.WriteLine("Enter quantities for Burger, Pizza, Drink.");
        Console.Write("Example: 2 1 3: ");

        List<int> quantities = Console.ReadLine()!
            .Split()
            .Select(int.Parse)
            .ToList();

        if (quantities.Count != menu.ProductCount)
        {
            Console.WriteLine("Invalid input. Please enter exactly 3 quantities.");
            return;
        }

        decimal totalPrice = menu.CalculateBill(quantities);

        if (menu.HasFreeDessert(totalPrice))
        {
            Console.WriteLine("Congratulations!");
            Console.WriteLine("You receive a free dessert!");
        }

        Console.WriteLine($"Final bill is: ${totalPrice}");
    }
}