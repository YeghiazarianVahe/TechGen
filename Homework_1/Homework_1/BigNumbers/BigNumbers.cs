using System.Text;

namespace Homework_1.BigNumbers
{
    class BigNumberMath
    {
        public static void Print()
        {
            
            int a = int.MaxValue;
            Console.WriteLine(unchecked(a + 1));  
        
            long b = long.MaxValue;
            Console.WriteLine(unchecked(b + 1));

            Console.WriteLine("-----------------------------");


            Console.WriteLine($"Add: {Add("-9999", "1")}");            
            Console.WriteLine($"Subtract: {Subtract("-10000", "1")}"); 
            Console.WriteLine($"Multiply: {Multiply("-123", "456")}"); 
        }

        // ==========================================
        // 1. PUBLIC ROUTERS
        // ==========================================

        private static string Add(string a, string b)
        {
            a = ValidateInput(a);
            b = ValidateInput(b);

            bool isNegA = a[0] == '-';
            bool isNegB = b[0] == '-';

            string absA = isNegA ? a.Substring(1) : a;
            string absB = isNegB ? b.Substring(1) : b;

            // Case 1: (+) + (+)
            if (!isNegA && !isNegB) return AddAbs(absA, absB);
            
            // Case 2: (-) + (-)
            if (isNegA && isNegB) return "-" + AddAbs(absA, absB);

            // Case3: (+) + (-)
            int cmp = CompareAbs(absA, absB);
            if (cmp == 0) return "0";

            if (isNegA && !isNegB) // -A + B
            {
                return cmp > 0 ? "-" + SubtractAbs(absA, absB) : SubtractAbs(absB, absA);
            }
            else // A + (-B)
            {
                return cmp > 0 ? SubtractAbs(absA, absB) : "-" + SubtractAbs(absB, absA);
            }
        }

        private static string Subtract(string a, string b)
        {
            a = ValidateInput(a);
            b = ValidateInput(b);

            if (b == "0") return a;

            // A - B same as A + (-B). Only change sign of b and call Add()
            string invertedB = b[0] == '-' ? b.Substring(1) : "-" + b;
            return Add(a, invertedB);
        }

        private static string Multiply(string a, string b)
        {
            a = ValidateInput(a);
            b = ValidateInput(b);

            if (a == "0" || b == "0") return "0";

            bool isNegA = a[0] == '-';
            bool isNegB = b[0] == '-';

            string absA = isNegA ? a.Substring(1) : a;
            string absB = isNegB ? b.Substring(1) : b;

            string result = MultiplyAbs(absA, absB);
            
            if (isNegA && !isNegB || !isNegA && isNegB)
            {
                return "-" + result;
            }

            return result;
        }

        // ==========================================
        // 2. PRIVATE ABSOLUTE MATH 
        // ==========================================

        private static string AddAbs(string a, string b)
        {
            StringBuilder result = new StringBuilder();
            int i = a.Length - 1, j = b.Length - 1, carry = 0;

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int digitA = i >= 0 ? a[i] - '0' : 0;
                int digitB = j >= 0 ? b[j] - '0' : 0;
                
                int sum = digitA + digitB + carry;
                result.Append((char)('0' + (sum % 10)));
                carry = sum / 10;

                i--; j--;
            }

            char[] resArray = result.ToString().ToCharArray();
            Array.Reverse(resArray);
            return new string(resArray);
        }

        private static string SubtractAbs(string a, string b)
        {
            // a >= b
            int maxLength = a.Length;
            char[] result = new char[maxLength];
            int i = a.Length - 1, j = b.Length - 1, k = maxLength - 1, borrow = 0;

            while (i >= 0)
            {
                int digitA = a[i] - '0';
                int digitB = j >= 0 ? b[j] - '0' : 0;
                int diff = digitA - digitB - borrow;

                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else borrow = 0;

                result[k] = (char)('0' + diff);
                i--; j--; k--;
            }

            // Remove 0. Ex: "100" - "99" = "001" -> "1"
            int start = 0;
            while (start < maxLength - 1 && result[start] == '0') start++;

            return new string(result, start, maxLength - start);
        }

        private static string MultiplyAbs(string a, string b)
        {
            int[] pos = new int[a.Length + b.Length];

            for (int i = a.Length - 1; i >= 0; i--)
            {
                for (int j = b.Length - 1; j >= 0; j--)
                {
                    int mul = (a[i] - '0') * (b[j] - '0');
                    int p1 = i + j, p2 = i + j + 1;
                    int sum = mul + pos[p2];

                    pos[p2] = sum % 10;
                    pos[p1] += sum / 10;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (int p in pos)
            {
                if (!(sb.Length == 0 && p == 0)) sb.Append(p);
            }

            return sb.Length == 0 ? "0" : sb.ToString();
        }

        // ==========================================
        // 3. UTILITY METHODS
        // ==========================================

        private static string ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return "0";
            
            // If only "-", it's not a number
            if (input == "-") return "0";

            for (int i = 0; i < input.Length; i++)
            {
                // Allow "-" sign only in front
                if (i == 0 && input[i] == '-') continue;
                
                if (input[i] < '0' || input[i] > '9') return "0";
            }
            return input;
        }

        private static int CompareAbs(string a, string b)
        {
            if (a.Length > b.Length) return 1;
            if (a.Length < b.Length) return -1;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > b[i]) return 1;
                if (a[i] < b[i]) return -1;
            }
            return 0;
        }
    }
}