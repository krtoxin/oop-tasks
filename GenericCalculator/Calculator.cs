using System;
using System.Numerics;

namespace GenericCalculator;

public class Calculator<T> where T : INumber<T>
{
    public T Add(T a, T b) => a + b;
    public T Subtract(T a, T b) => a - b;
    public T Multiply(T a, T b) => a * b;
    public T Divide(T a, T b)
    {
        if (b == T.Zero) throw new DivideByZeroException();
        return a / b;
    }

    public T Pow(T value, int exponent)
    {
        if (exponent == 0) return T.One;
        if (exponent < 0)
        {
            if (typeof(T) == typeof(int))
            {
                throw new ArgumentOutOfRangeException(nameof(exponent), "Negative exponent not supported for integral types.");
            }
            return T.One / Pow(value, -exponent);
        }
        T result = T.One;
        T baseVal = value;
        int exp = exponent;
        while (exp > 0)
        {
            if ((exp & 1) == 1)
            {
                result *= baseVal;
            }
            baseVal *= baseVal;
            exp >>= 1;
        }
        return result;
    }
}
