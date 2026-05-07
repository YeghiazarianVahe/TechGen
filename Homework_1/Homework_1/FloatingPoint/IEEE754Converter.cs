
namespace Homework_1.FloatingPoint;

public class Ieee754Converter
{
    public static int[] FloatToIeee754(float value)
    {
        int[] bits = new int[32];

        if (float.IsNaN(value))
        {
            bits[0] = 0;
            SetExponent(bits, 255);
            bits[9] = 1; 
            return bits;
        }

        if (float.IsInfinity(value))
        {
            bits[0] = value < 0 ? 1 : 0;
            SetExponent(bits, 255);
            return bits;
        }

        if (value < 0)
        {
            bits[0] = 1;
            value = -value;
        }

        if (value == 0)
        {
            return bits;
        }

        int exponent = 0;
        float normalized = value;

        while (normalized >= 2.0f)
        {
            normalized /= 2.0f;
            exponent++;
        }

        while (normalized < 1.0f)
        {
            normalized *= 2.0f;
            exponent--;
        }

        int biasedExponent = exponent + 127;

        SetExponent(bits, biasedExponent);

        float fraction = normalized - 1.0f;

        for (int i = 9; i < 32; i++)
        {
            fraction *= 2.0f;

            if (fraction >= 1.0f)
            {
                bits[i] = 1;
                fraction -= 1.0f;
            }
        }

        return bits;
    }

    public static float Ieee754ToFloat(int[] bits)
    {
        if (bits == null || bits.Length != 32)
            throw new ArgumentException("IEEE 754 array must contain exactly 32 bits.");

        int sign = bits[0] == 0 ? 1 : -1;

        int exponentBits = 0;
        for (int i = 1; i <= 8; i++)
        {
            exponentBits = exponentBits * 2 + bits[i];
        }

        float fraction = 0.0f;
        float power = 0.5f;

        for (int i = 9; i < 32; i++)
        {
            if (bits[i] == 1)
            {
                fraction += power;
            }

            power /= 2.0f;
        }

        if (exponentBits == 0 && fraction == 0.0f)
        {
            return bits[0] == 1 ? -0.0f : 0.0f;
        }

        if (exponentBits == 255)
        {
            if (fraction == 0.0f)
                return sign == 1 ? float.PositiveInfinity : float.NegativeInfinity;

            return float.NaN;
        }

        if (exponentBits == 0)
        {
            return sign * fraction * Pow2(-126);
        }

        int realExponent = exponentBits - 127;
        return sign * (1.0f + fraction) * Pow2(realExponent);
    }

    private static float Pow2(int exponent)
    {
        float result = 1.0f;

        if (exponent > 0)
        {
            for (int i = 0; i < exponent; i++)
                result *= 2.0f;
        }
        else
        {
            for (int i = 0; i < -exponent; i++)
                result /= 2.0f;
        }

        return result;
    }
    private static void SetExponent(int[] bits, int exponent)
    {
        for (int i = 8; i >= 1; i--)
        {
            bits[i] = exponent % 2;
            exponent /= 2;
        }
    }
    
}