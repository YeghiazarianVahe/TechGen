namespace OnlineShop.Models.Products;
using Interfaces;

public class DigitalProduct : Product, IDownloadable
{
    private int _fileSizeMb;
    public int FileSizeMb
    {
        get => _fileSizeMb;
        set
        {
            if(value <= 0) throw new ArgumentOutOfRangeException(nameof(FileSizeMb));
            _fileSizeMb = value;
        }
    }

    public DigitalProduct(string name, decimal price, int fileSizeMb) : base(name, price)
    {
        FileSizeMb = fileSizeMb;
    }

    public override string GetDescription()
    {
        return $"Digital product {Name}, file size: {FileSizeMb}MB";
    }

    public string GetDownloadUrl()
    {
        return $"https://downloads.shop.com/{Name.ToLower().Replace(' ', '-')}";
    }
    
}
