using System;
using System.Numerics;
using GenericCalculator;

static T ClientIntegerOp<T>(Calculator<T> calc, T a, T b) where T : IBinaryInteger<T>
{
    return calc.Add(a, b);
}

static T ClientFloatingOp<T>(Calculator<T> calc, T a, T b) where T : IFloatingPoint<T>
{
    return calc.Divide(a, b);
}

var intCalc = new Calculator<int>();
var doubleCalc = new Calculator<double>();

Console.WriteLine("Integer add via client method: " + ClientIntegerOp(intCalc, 3, 5));
Console.WriteLine("Floating divide via client method: " + ClientFloatingOp(doubleCalc, 7.5, 2.5));

Console.WriteLine("Pow int 2^10: " + intCalc.Pow(2, 10));
Console.WriteLine("Pow double 2^(-3): " + doubleCalc.Pow(2.0, -3));
