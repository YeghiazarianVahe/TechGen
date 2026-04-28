using System.Text;  // Required for StringBuilder

namespace Homework_1.FloatingPoint;

public static class FloatingPointBinaryConverter
{
    private const int FloatTotalBits = 32;
    private const int FloatExponentBits = 8;
    private const int FloatMantissaBits = 23;
    private const int FloatBias = 127;

    private const int DoubleTotalBits = 64;
    private const int DoubleExponentBits = 11;
    private const int DoubleMantissaBits = 52;
    private const int DoubleBias = 1023;

    public static string FloatToBinary(float value, bool formatting = false)
    {
        string binary = ToIeee754Binary(
            value,
            FloatExponentBits,
            FloatMantissaBits,
            FloatBias);

        return formatting
            ? FormatBinary(binary, FloatExponentBits, FloatMantissaBits)
            : binary;
    }

    public static string DoubleToBinary(double value, bool formatting = false)
    {
        string binary = ToIeee754Binary(
            value,
            DoubleExponentBits,
            DoubleMantissaBits,
            DoubleBias);

        return formatting
            ? FormatBinary(binary, DoubleExponentBits, DoubleMantissaBits)
            : binary;
    }

    public static float BinaryToFloat(string value)
    {
        string binary = CleanBinaryInput(value);
        ValidateLength(binary, FloatTotalBits, nameof(value));

        double result = FromIeee754Binary(
            binary,
            FloatExponentBits,
            FloatMantissaBits,
            FloatBias);

        return (float)result;
    }

    public static double BinaryToDouble(string value)
    {
        string binary = CleanBinaryInput(value);
        ValidateLength(binary, DoubleTotalBits, nameof(value));

        return FromIeee754Binary(
            binary,
            DoubleExponentBits,
            DoubleMantissaBits,
            DoubleBias);
    }

    private static string ToIeee754Binary(
        double value,
        int exponentBits,
        int mantissaBits,
        int bias)
    {
        bool isNegative = IsNegative(value);

        if (double.IsNaN(value))
        {
            return BuildSpecialValue(
                isNegative,
                exponentBits,
                mantissaBits,
                mantissaStartsWithOne: true);
        }

        if (double.IsInfinity(value))
        {
            return BuildSpecialValue(
                isNegative,
                exponentBits,
                mantissaBits,
                mantissaStartsWithOne: false);
        }

        if (value == 0.0)
        {
            return BuildZero(isNegative, exponentBits, mantissaBits);
        }

        double absoluteValue = Math.Abs(value);
        int maxExponentValue = PowerOfTwo(exponentBits) - 1;
        int minNormalPower = 1 - bias;
        double minNormalValue = Math.Pow(2, minNormalPower);

        if (absoluteValue < minNormalValue)
        {
            return BuildSubnormal(
                isNegative,
                absoluteValue,
                exponentBits,
                mantissaBits,
                bias);
        }

        int exponentPower = NormalizeToRangeOneToTwo(ref absoluteValue);
        int exponentValue = exponentPower + bias;

        if (exponentValue >= maxExponentValue)
        {
            return BuildSpecialValue(
                isNegative,
                exponentBits,
                mantissaBits,
                mantissaStartsWithOne: false);
        }

        double fraction = absoluteValue - 1.0;
        ulong mantissaValue = RoundToMantissaInteger(fraction, mantissaBits);
        ulong mantissaLimit = PowerOfTwoUnsigned(mantissaBits);

        if (mantissaValue == mantissaLimit)
        {
            exponentValue++;
            mantissaValue = 0;

            if (exponentValue >= maxExponentValue)
            {
                return BuildSpecialValue(
                    isNegative,
                    exponentBits,
                    mantissaBits,
                    mantissaStartsWithOne: false);
            }
        }

        string signBit = isNegative ? "1" : "0";
        string exponentBinary = ToBinaryUnsigned((ulong)exponentValue, exponentBits);
        string mantissaBinary = ToBinaryUnsigned(mantissaValue, mantissaBits);

        return signBit + exponentBinary + mantissaBinary;
    }

    private static double FromIeee754Binary(
        string binary,
        int exponentBits,
        int mantissaBits,
        int bias)
    {
        bool isNegative = binary[0] == '1';

        string exponentBinary = binary.Substring(1, exponentBits);
        string mantissaBinary = binary.Substring(1 + exponentBits, mantissaBits);

        ulong exponentValue = ReadBinaryUnsigned(exponentBinary);
        ulong mantissaValue = ReadBinaryUnsigned(mantissaBinary);

        ulong maxExponentValue = PowerOfTwoUnsigned(exponentBits) - 1;

        if (exponentValue == maxExponentValue)
        {
            if (mantissaValue == 0)
            {
                return isNegative
                    ? double.NegativeInfinity
                    : double.PositiveInfinity;
            }

            return double.NaN;
        }

        if (exponentValue == 0 && mantissaValue == 0)
        {
            return isNegative ? -0.0 : 0.0;
        }

        double sign = isNegative ? -1.0 : 1.0;
        double mantissaDenominator = Math.Pow(2, mantissaBits);

        if (exponentValue == 0)
        {
            double subnormalMantissa = mantissaValue / mantissaDenominator;
            int subnormalPower = 1 - bias;

            return sign * subnormalMantissa * Math.Pow(2, subnormalPower);
        }

        double normalMantissa = 1.0 + mantissaValue / mantissaDenominator;
        int power = (int)exponentValue - bias;

        return sign * normalMantissa * Math.Pow(2, power);
    }

    private static int NormalizeToRangeOneToTwo(ref double value)
    {
        int exponentPower = 0;

        while (value >= 2.0)
        {
            value /= 2.0;
            exponentPower++;
        }

        while (value < 1.0)
        {
            value *= 2.0;
            exponentPower--;
        }

        return exponentPower;
    }

    private static string BuildSubnormal(
        bool isNegative,
        double absoluteValue,
        int exponentBits,
        int mantissaBits,
        int bias)
    {
        int minNormalPower = 1 - bias;

        double scaledMantissa =
            absoluteValue / Math.Pow(2, minNormalPower) * Math.Pow(2, mantissaBits);

        ulong mantissaValue = (ulong)Math.Round(
            scaledMantissa,
            MidpointRounding.ToEven);

        ulong mantissaLimit = PowerOfTwoUnsigned(mantissaBits);

        string signBit = isNegative ? "1" : "0";

        if (mantissaValue == mantissaLimit)
        {
            string exponentBinary = ToBinaryUnsigned(1, exponentBits);
            string mantissaBinary = ToBinaryUnsigned(0, mantissaBits);

            return signBit + exponentBinary + mantissaBinary;
        }

        string exponent = new string('0', exponentBits);
        string mantissa = ToBinaryUnsigned(mantissaValue, mantissaBits);

        return signBit + exponent + mantissa;
    }

    private static string BuildZero(
        bool isNegative,
        int exponentBits,
        int mantissaBits)
    {
        string signBit = isNegative ? "1" : "0";
        string exponent = new string('0', exponentBits);
        string mantissa = new string('0', mantissaBits);

        return signBit + exponent + mantissa;
    }

    private static string BuildSpecialValue(
        bool isNegative,
        int exponentBits,
        int mantissaBits,
        bool mantissaStartsWithOne)
    {
        string signBit = isNegative ? "1" : "0";
        string exponent = new string('1', exponentBits);

        string mantissa = mantissaStartsWithOne
            ? "1" + new string('0', mantissaBits - 1)
            : new string('0', mantissaBits);

        return signBit + exponent + mantissa;
    }

    private static ulong RoundToMantissaInteger(double fraction, int mantissaBits)
    {
        double scaled = fraction * Math.Pow(2, mantissaBits);

        return (ulong)Math.Round(scaled, MidpointRounding.ToEven);
    }

    private static string ToBinaryUnsigned(ulong value, int width)
    {
        char[] bits = new char[width];

        for (int i = width - 1; i >= 0; i--)
        {
            ulong remainder = value % 2;
            bits[i] = remainder == 1 ? '1' : '0';

            value /= 2;
        }

        return new string(bits);
    }

    private static ulong ReadBinaryUnsigned(string binary)
    {
        ulong result = 0;

        foreach (char bit in binary)
        {
            result *= 2;

            if (bit == '1')
            {
                result += 1;
            }
        }

        return result;
    }

    private static int PowerOfTwo(int power)
    {
        int result = 1;

        for (int i = 0; i < power; i++)
        {
            result *= 2;
        }

        return result;
    }

    private static ulong PowerOfTwoUnsigned(int power)
    {
        ulong result = 1;

        for (int i = 0; i < power; i++)
        {
            result *= 2;
        }

        return result;
    }

    private static string CleanBinaryInput(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Input cannot be null, empty, or whitespace.",
                nameof(value));
        }

        StringBuilder builder = new();

        foreach (char character in value)
        {
            if (character == '0' || character == '1')
            {
                builder.Append(character);
            }
        }

        return builder.ToString();
    }

    private static void ValidateLength(
        string binary,
        int expectedLength,
        string parameterName)
    {
        if (binary.Length != expectedLength)
        {
            throw new ArgumentException(
                $"Binary value must contain exactly {expectedLength} bits.",
                parameterName);
        }
    }

    private static string FormatBinary(
        string binary,
        int exponentBits,
        int mantissaBits)
    {
        string sign = binary.Substring(0, 1);
        string exponent = binary.Substring(1, exponentBits);
        string mantissa = binary.Substring(1 + exponentBits, mantissaBits);

        return $"{sign} | {exponent} | {mantissa}";
    }

    private static bool IsNegative(double value)
    {
        return value < 0.0 || value == 0.0 && 1.0 / value < 0.0;
    }
}