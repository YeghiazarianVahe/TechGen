using System.Runtime.CompilerServices;
using System.Text;

namespace ShopingCalc;

class User
{
    public string Name { get; set; }
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
}

class Product
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Price: {Price}\n";
    }
}

class Store
{
    private Dictionary<Product, int> _products;

    public Store()
    {
        _products = new Dictionary<Product, int>();
    }

    public void AddProduct(Product p, int count)
    {
        _products.Add(p, count);
    }

    public Product GiveProduct(string name, int quantity)
    {
        foreach (KeyValuePair<Product, int> kvp in _products)
        {
            if (kvp.Key.Name == name && kvp.Value >= quantity)
                return new Product(name, quantity);
        }
        throw new Exception("Product not found");
    }

    public override string ToString()
    {
        StringBuilder store = new StringBuilder();
        foreach (KeyValuePair<Product, int> kvp in _products)
        {
            store.AppendLine($"Name: {kvp.Key.Name}, Price: {kvp.Value}");
        }
        return store.ToString();
    }

}

class Bill
{
    private Dictionary<Product, int> _products;
    Guid Id = Guid.NewGuid();

    public Bill(Dictionary<Product, int> products)
    {
        _products = products;
    }

    private int _totalPrice = 0;
    float _discount = 0;
    private float _finalPrice = 0;

    public int TotalPrice
    {
        get => _totalPrice;
        set => _totalPrice = value;
    }

    public void CalculateTotal()
    {
        int price = 0;
        foreach (KeyValuePair<Product, int> kvp in _products)
        {
            price += kvp.Key.Price * kvp.Value;
        }

        TotalPrice = price;
    }
    
    
    private bool HaveDiscount()
    {
        return TotalPrice > 1000;
    }
    
    private void MakeDiscount()
    {
        _discount = 0.1f * TotalPrice;
    }

    private void CalculateFinalPrice()
    {
        bool haveDiscount = HaveDiscount();
        if (haveDiscount)
            MakeDiscount();
        _finalPrice = TotalPrice - _discount;
    }

    public override string ToString()
    {
        StringBuilder productList = new StringBuilder();
        productList.AppendLine("---------------------------------");
        productList.AppendLine($"----------- {Id.ToString()} ------------");
        productList.AppendLine("---------------------------------");
        foreach (KeyValuePair<Product, int> kvp in _products)
        {
            productList.AppendLine($"Product: {kvp.Key.Name}, Quantity: {kvp.Value}");
        }
        CalculateTotal();
        bool haveDiscount = HaveDiscount();
        if (haveDiscount)
            MakeDiscount();
        CalculateFinalPrice();
        productList.AppendLine("Price: " + TotalPrice.ToString());
        productList.AppendLine("Discount: " + _discount.ToString());
        
        productList.AppendLine("---------------------------------");
        productList.AppendLine($"For payment: {_finalPrice}");
        return productList.ToString();
    }

}

class Order
{
    private Dictionary<Product, int> _products;
    private Bill bill;
    public Order()
    {
        _products = new Dictionary<Product, int>();
        bill = new Bill(_products);
    }

    public void AddProduct(Product p, int count)
    {
        _products.Add(p, count);
    }

    public override string ToString()
    {
        return bill.ToString();
    }
}


class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();
        store.AddProduct(new Product("Laptop", 500), 6);
        store.AddProduct(new Product("Mouse", 25), 3);
        store.AddProduct(new Product("Keyboard", 25), 2);

       
        
        Order order = new Order();
        order.AddProduct(new Product("Laptop", 500), 2);
        order.AddProduct(new Product("Mouse", 25), 3);
        
        Console.WriteLine(order.ToString());
        

    }
}
