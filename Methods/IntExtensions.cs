using System;

public static class IntExtensions
{
    public static int DigitCount(this int number)
    {
        return Math.Abs(number).ToString().Length;
    }

    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }

    public static bool IsOdd(this int number)
    {
        return number % 2 != 0;
    }

    public static bool IsGreaterThan(this int number, int value)
    {
        return number > value;
    }

    public static bool IsLessThan(this int number, int value)
    {
        return number < value;
    }

    public static bool IsEqualTo(this int number, int value)
    {
        return number == value;
    }
}
