using Homework_1.FloatingPoint;
using Homework_1.BigNumbers;
using Homework_1.MyList;

namespace Homework_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Converter();
            BigNumber();
            MyListDemo();
        }
        

        // Task 1
        private static void Converter()
        {
            PrintHeader();
            RunFloatExample();
            RunDoubleExample();
            RunSpecialValues();
            RunManualInput();
        }

        private static void PrintHeader()
        {
            Console.WriteLine("IEEE 754 Floating Point Binary Converter");
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
        }

        private static void RunFloatExample()
        {
            Console.WriteLine("Float to Binary");
            Console.WriteLine("----------------");

            float value1 = 12.5f;
            float value2 = -3.14f;

            string binary1 = FloatingPointBinaryConverter.FloatToBinary(value1);
            string formattedBinary1 = FloatingPointBinaryConverter.FloatToBinary(value1, true);
            
            string binary2 = FloatingPointBinaryConverter.FloatToBinary(value2);
            string formattedBinary2 = FloatingPointBinaryConverter.FloatToBinary(value2, true);
            
            Console.WriteLine($"Float value1: {value1}");
            Console.WriteLine($"Binary value1: {binary1}");
            Console.WriteLine($"Formatted binary value1: {formattedBinary1}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Float value2: {value2}");
            Console.WriteLine($"Binary value2: {binary2}");
            Console.WriteLine($"Formatted binary value2: {formattedBinary2}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            
            Console.WriteLine("Binary to Float");
            Console.WriteLine("-------------------");

            float restoredValue1 = FloatingPointBinaryConverter.BinaryToFloat(binary1);
            float restoredValue2 = FloatingPointBinaryConverter.BinaryToFloat(binary2);
            
            Console.WriteLine($"Float value1: {restoredValue1}");
            Console.WriteLine($"Float value2: {restoredValue2}");
            Console.WriteLine();
        }

        private static void RunDoubleExample()
        {
            Console.WriteLine("Double to Binary");
            Console.WriteLine("----------------");

            double value1 = 16.2547;
            double value2 = -3.14;

            string binary1 = FloatingPointBinaryConverter.DoubleToBinary(value1);
            string formattedBinary1 = FloatingPointBinaryConverter.DoubleToBinary(value1, true);
            
            string binary2 = FloatingPointBinaryConverter.DoubleToBinary(value2);
            string formattedBinary2 = FloatingPointBinaryConverter.DoubleToBinary(value2, true);
            
            Console.WriteLine($"Double value1: {value1}");
            Console.WriteLine($"Binary value1: {binary1}");
            Console.WriteLine($"Formatted binary value1: {formattedBinary1}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Double value2: {value2}");
            Console.WriteLine($"Binary value2: {binary2}");
            Console.WriteLine($"Formatted binary value2: {formattedBinary2}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            
            Console.WriteLine("Binary to Double");
            Console.WriteLine("-------------------");

            double restoredValue1 = FloatingPointBinaryConverter.BinaryToDouble(binary1);
            double restoredValue2 = FloatingPointBinaryConverter.BinaryToDouble(binary2);
            
            Console.WriteLine($"Double value1: {restoredValue1}");
            Console.WriteLine($"Double value2: {restoredValue2}");
            Console.WriteLine();
        }

        private static void RunSpecialValues()
        {
            Console.WriteLine("SPECIAL FLOAT VALUES");
            Console.WriteLine("--------------------");

            float positiveZero = 0.0f;
            float negativeZero = -0.0f;
            float positiveInfinity = float.PositiveInfinity;
            float negativeInfinity = float.NegativeInfinity;
            float notANumber = float.NaN;

            Console.WriteLine($"+0.0f:       {FloatingPointBinaryConverter.FloatToBinary(positiveZero, formatting: true)}");
            Console.WriteLine($"-0.0f:       {FloatingPointBinaryConverter.FloatToBinary(negativeZero, formatting: true)}");
            Console.WriteLine($"+Infinity:  {FloatingPointBinaryConverter.FloatToBinary(positiveInfinity, formatting: true)}");
            Console.WriteLine($"-Infinity:  {FloatingPointBinaryConverter.FloatToBinary(negativeInfinity, formatting: true)}");
            Console.WriteLine($"NaN:        {FloatingPointBinaryConverter.FloatToBinary(notANumber, formatting: true)}");
            Console.WriteLine();
        }

        private static void RunManualInput()
        {
            Console.WriteLine("MANUAL BINARY INPUT EXAMPLE");
            Console.WriteLine("---------------------------");

            string binary = "0 | 10000010 | 10010000000000000000000";

            float value = FloatingPointBinaryConverter.BinaryToFloat(binary);

            Console.WriteLine($"Input binary: {binary}");
            Console.WriteLine($"Float value:  {value}");
            Console.WriteLine();
        }

        private static void PrintFooter()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("End of IEEE754 Converter");
        }
        
        // Task 2

        private static void BigNumber()
        {
            Console.WriteLine("Big Number");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            
            BigNumberMath.Print();
            Console.WriteLine("End of Big Number");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }
        
        // Task 3

        private static void MyListDemo()
        {
            Console.WriteLine("My List");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            
            MyList.MyList list = new MyList.MyList();
            Console.WriteLine("Add items");
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine($"Items count after add: {list.Count}");
            
            Console.WriteLine("Add range");
            int[] newElems = { 10, 20, 30 };
            list.AddRange(newElems);
            Console.WriteLine($"Items count after update: {list.Count}");
            
            Console.WriteLine("TryGet");
            if (list.TryGet(2, out int value))
            {
                Console.WriteLine($"Item with 2 index is: {value}");
            }

            if (!list.TryGet(10, out int invalidValue))
            {
                Console.WriteLine("There are no item with 10 index!");
            }

            Console.WriteLine("IndexOf");
            int index = list.IndexOf(20);
            Console.WriteLine($"Index of item with value 20 is: {index}");
            
            Console.WriteLine("Remove");
            bool isRemoved = list.Remove(20);
            Console.WriteLine($"Item with 20 index is: {isRemoved}. New list item count is: {list.Count}");
            
            
            Console.WriteLine("Print list");
            list.Print();
        }
    }
}