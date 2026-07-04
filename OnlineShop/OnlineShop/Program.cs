using OnlineShop.Models.Products;
using OnlineShop.Models.Customers;
using OnlineShop.Models.Cart;
using OnlineShop.Models.Orders;

class Program
{
    static void Main(string[] args)
    {
        
// --- Products ---
        var laptop = new PhysicalProduct("Laptop", 999.99m, 2.5m);
        var usb = new PhysicalProduct("USB", 25.2m, 1.8m);
        var ebook = new DigitalProduct("Ebook", 25.2m, 12);

        Console.WriteLine("=== Products ===");
        Console.WriteLine(laptop.GetDescription());
        Console.WriteLine($"Shipping: ${laptop.CalculateShipping():F2}");
        Console.WriteLine(ebook.GetDescription());
        Console.WriteLine($"Download: {ebook.GetDownloadUrl()}");

// --- Cart ---
        var cart = new ShoppingCart(5);
        cart.AddItem(laptop, 3);
        cart.AddItem(usb, 2);
        Console.WriteLine("\n" + cart);

// --- Checkout ---
        var order = cart.Checkout(1);
        Console.WriteLine("\n=== Order ===");
        Console.WriteLine(order);

// --- Customer places order ---
        var customer = new Customer("Vahe", "test@sda.a", new Address("Hrazdan"));
        customer.PlaceOrder(order);
        Console.WriteLine($"\n{customer.Name} has {customer.OrderCount} order(s).");

// --- Error case ---
        try
        {
            cart.Checkout(99);  // cart was cleared — what happens?
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\n[Expected error] {ex.Message}");
        }
    }
}