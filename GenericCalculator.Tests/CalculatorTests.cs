using System;
using GenericCalculator;
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_Int_Works()
    {
        var calc = new Calculator<int>();
        Assert.Equal(7, calc.Add(3, 4));
    }

    [Fact]
    public void Subtract_Int_Works()
    {
        var calc = new Calculator<int>();
        Assert.Equal(-1, calc.Subtract(3, 4));
    }

    [Fact]
    public void Multiply_Int_Works()
    {
        var calc = new Calculator<int>();
        Assert.Equal(12, calc.Multiply(3, 4));
    }

    [Fact]
    public void Divide_Int_Works()
    {
        var calc = new Calculator<int>();
        Assert.Equal(2, calc.Divide(8, 4));
    }

    [Fact]
    public void Pow_Int_Positive_Works()
    {
        var calc = new Calculator<int>();
        Assert.Equal(1024, calc.Pow(2, 10));
    }

    [Fact]
    public void Pow_Int_Negative_Throws()
    {
        var calc = new Calculator<int>();
        Assert.Throws<ArgumentOutOfRangeException>(() => calc.Pow(2, -1));
    }

    [Fact]
    public void Add_Decimal_Works()
    {
        var calc = new Calculator<decimal>();
        Assert.Equal(7.5m, calc.Add(3.0m, 4.5m));
    }

    [Fact]
    public void Subtract_Decimal_Works()
    {
        var calc = new Calculator<decimal>();
        Assert.Equal(-1.5m, calc.Subtract(3.0m, 4.5m));
    }

    [Fact]
    public void Multiply_Double_Works()
    {
        var calc = new Calculator<double>();
        Assert.Equal(13.5, calc.Multiply(4.5, 3.0), 10);
    }

    [Fact]
    public void Divide_Double_Works()
    {
        var calc = new Calculator<double>();
        Assert.Equal(2.5, calc.Divide(5.0, 2.0), 10);
    }

    [Fact]
    public void Pow_Double_Negative_Works()
    {
        var calc = new Calculator<double>();
        Assert.Equal(0.125, calc.Pow(2.0, -3), 10);
    }
}
