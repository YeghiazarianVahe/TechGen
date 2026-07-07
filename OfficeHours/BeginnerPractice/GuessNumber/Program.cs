namespace GuessNumber;

class Program
{
    public static void Guess()
    {
        int secret = new Random().Next(0, 100);

        bool isCorrect = false;
        while (!isCorrect)
        {
            Console.Write("Please enter a number between 1 and 100: ");
            int guessedNumber = Convert.ToInt32(Console.ReadLine());
            if (guessedNumber == secret)
            {
                isCorrect = true;
                Congratulation();
            }

            else if (guessedNumber > secret)
            {
                Console.WriteLine("Too high!");
            }

            else
            {
                Console.WriteLine("Too low!");
            }
        }
    }

    private static void Congratulation()
    {
        Console.WriteLine("Correct!");
        return;
    }
    
    static void Main(string[] args)
    {
        Guess();
    }
}