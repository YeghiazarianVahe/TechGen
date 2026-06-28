namespace Algo;

internal static class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 1, 2, -5, 6, 0, -10 };
        Sort.InsertionSort(arr);
        // Utilities.PrintArray(arr);
        Console.WriteLine(Searching.BinarySearch([0,1,2,3,4,5,6,7,8,9], 9));
        Console.WriteLine(Searching.BinarySearchRecursive([1,2,3,4,5,6,7,8,9,10], 5));
    }
}

static class Utilities
{
    public static void Swap(ref int a, ref int b)
    {
        (a, b) = (b, a);
    }

    public static void PrintArray(int[] array)
    {
        for(int i = 0; i < array.Length; i++)
            Console.Write(array[i] + " ");
    }
}

static class Sort
{
    public static void SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }

            if (minIndex != i) 
                Utilities.Swap(ref array[i], ref array[minIndex]);
        }
    }

    public static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            bool swapped = false;
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Utilities.Swap(ref array[j], ref array[j + 1]);
                    swapped = true;
                }
            }

            if (!swapped)
                break;
        }
    }

    public static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
}

static class Searching
{
    public static int BinarySearch(int[] array, int target)
    {
        int l = 0;
        int r = array.Length - 1;
        while (l <= r)
        {
            int mid = l + (r - l) / 2;
            if (array[mid] == target) return mid;
            else if (array[mid] > target) r =  mid - 1;
            else l = mid + 1;
        }
        return -1;
    }

    public static int BinarySearchRecursive(int[] array, int target, int left = 0, int right = -1)
    {
        if (right == -1) right = array.Length - 1;
        if (left > right) return -1;
        int mid = left + (right - left) / 2;
        if (array[mid] == target) return mid;
        if (array[mid] > target) return BinarySearchRecursive(array, target, left, mid - 1);
        else return BinarySearchRecursive(array, target, mid + 1, right);
    }
}