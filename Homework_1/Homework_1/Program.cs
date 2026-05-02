using floatPoint = Homework_1.FloatingPoint.FloatingPointBinaryConverter;
using bigNumbers = Homework_1.BigNumbers.BigNumberMath;
using customList = Homework_1.MyList.MyList;

namespace Homework_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Converter();
            BigNumber();
            MyListDemo();
            
            Console.WriteLine();
            Console.WriteLine("All tasks finished.");
        }
        

        //  /====================================================\
        //  /=========== CONSOLE HELPER FUNCTIONS ===============\
        //  /====================================================\
        
        private static void RunNewTask(string section)
        {
            Console.WriteLine("****************************************");
            Console.WriteLine($"*****   {section}      *****");
            Console.WriteLine("****************************************");
            Console.WriteLine();
        }

        private static void EndTask()
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("********** Task completed! *************");
            Console.WriteLine("****************************************");
        }
        
        //  /====================================================\
        //  /==================== TASK 1 ========================\
        //  /====================================================\
        private static void Converter()
        {
            RunNewTask("Testing IEEE 754  Floating Point Binary Converter");
            RunFloatExample();
            RunDoubleExample();
            RunSpecialValues();
            RunManualInput();
            EndTask();
        }



        private static void RunFloatExample()
        {
            Console.WriteLine("Float to Binary");
            Console.WriteLine("----------------");

            float value1 = 12.5f;
            float value2 = -3.14f;

            string binary1 = floatPoint.FloatToBinary(value1);
            string formattedBinary1 = floatPoint.FloatToBinary(value1, true);
            
            string binary2 = floatPoint.FloatToBinary(value2);
            string formattedBinary2 = floatPoint.FloatToBinary(value2, true);
            
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

            float restoredValue1 = floatPoint.BinaryToFloat(binary1);
            float restoredValue2 = floatPoint.BinaryToFloat(binary2);
            
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

            string binary1 = floatPoint.DoubleToBinary(value1);
            string formattedBinary1 = floatPoint.DoubleToBinary(value1, true);
            
            string binary2 = floatPoint.DoubleToBinary(value2);
            string formattedBinary2 = floatPoint.DoubleToBinary(value2, true);
            
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

            double restoredValue1 = floatPoint.BinaryToDouble(binary1);
            double restoredValue2 = floatPoint.BinaryToDouble(binary2);
            
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

            Console.WriteLine($"+0.0f:      {floatPoint.FloatToBinary(positiveZero, formatting: true)}");
            Console.WriteLine($"-0.0f:      {floatPoint.FloatToBinary(negativeZero, formatting: true)}");
            Console.WriteLine($"+Infinity:  {floatPoint.FloatToBinary(positiveInfinity, formatting: true)}");
            Console.WriteLine($"-Infinity:  {floatPoint.FloatToBinary(negativeInfinity, formatting: true)}");
            Console.WriteLine($"NaN:        {floatPoint.FloatToBinary(notANumber, formatting: true)}");
            Console.WriteLine();
        }

        private static void RunManualInput()
        {
            Console.WriteLine("MANUAL BINARY INPUT EXAMPLE");
            Console.WriteLine("---------------------------");

            string binary = "0 | 10000010 | 10010000000000000000000";

            float value = floatPoint.BinaryToFloat(binary);

            Console.WriteLine($"Input binary: {binary}");
            Console.WriteLine($"Float value:  {value}");
            Console.WriteLine();
        }
        
        //  /====================================================\
        //  /==================== TASK 2 ========================\
        //  /====================================================\

        private static void BigNumber()
        {
            RunNewTask("Big Numbers");
            bigNumbers.Print();
            EndTask();
        }
        
        //  /====================================================\
        //  /==================== TASK 3 ========================\
        //  /====================================================\

        private static void MyListDemo()
        {
            RunNewTask("My List Demo");
    
            customList list = new customList();
            Console.WriteLine($"Initial list size is: {list.Count}");
    
            Console.WriteLine("\n--- Adding items ---");
            for (int i = 0; i < 10; ++i)
            {
                list.Add(i);
            }
            
            list.Print();
            Console.WriteLine($"List size after additions: {list.Count}");
    
            Console.WriteLine("\n--- Removing items ---");
            bool removedFive = list.Remove(5);
            Console.WriteLine($"Removed item '5': {removedFive}");
    
            bool removedNinetyNine = list.Remove(99);
            Console.WriteLine($"Removed item '99': {removedNinetyNine}");
    
            list.Print();
            Console.WriteLine($"List size after removals is: {list.Count}");

            Console.WriteLine("\n--- Testing Search & Retrieval ---");
            Console.WriteLine($"List contains '7': {list.Contains(7)}");
            Console.WriteLine($"Index of item '8': {list.IndexOf(8)}");

            if (list.TryGet(3, out int val))
            {
                Console.WriteLine($"Successfully retrieved value at index 3: {val}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve value at index 3 (Out of bounds).");
            }

            Console.WriteLine("\n--- Clearing the list ---");
            list.Clear();
            Console.WriteLine($"List size after Clear(): {list.Count}");
            EndTask();
        }
    }
}