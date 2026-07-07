namespace MultiplicationTable;

class Program
{
    static void PrintTable(int num)
    {
        for (int i = 1; i <= 10; i++)
            Console.WriteLine($"{num} * {i} = {num * i}");
    }

    static void Main(string[] args)
    {
        Console.Write("Enter a number: ");
        bool isValid = int.TryParse(Console.ReadLine(), out int num);
        if(isValid)
            PrintTable(num);
        else
            Console.WriteLine("Invalid input!");
    }
}