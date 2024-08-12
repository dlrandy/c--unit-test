using Sparky_lib;

namespace Sparky_XUnit_Test;
 
public class CalculatorNUnitTests
{
    
    public void Setup()
    {
        Console.WriteLine("Set up....");
    }
    [Fact]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        // Arrange
        Calculator calc = new();


        // Act
        int result = calc.AddNumbers(10, 20);

        // Assert
        Assert.Equal(30, result);
    }
    [Theory]
    [InlineData(5.4,10.5)] // 15.9
    [InlineData(5.43,10.53)] // 15.96
    [InlineData(5.49,10.59)] // 16.08
    public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a, double b)
    {
        // Arrange
        Calculator calc = new();


        // Act
        double result = calc.AddNumbersDouble(a,b);

        // Assert
        Assert.Equal(15.9, result, 0);
    }
    [Fact]
    public void IsOddChecker_InputEvenNumber_ReruenFalse()
    {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(10);
        Assert.Equal(false,isOdd);
        Assert.False(isOdd);
    }

    [Fact]
    public void IsOddChecker_InputOddNumber_ReruenTrue()
    {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(11);
        Assert.True(isOdd);

    }


    [Theory]
    [InlineData(11, true)]
    [InlineData(13, true)]
    public void IsOddChecker_InputDynamicOddNumber_ReruenTrue(int a, bool isExpected)
    {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(a);
        Assert.Equal(isOdd, isExpected);
    }

    [Theory]
    [InlineData(10,  false)]
    [InlineData(11,  true)]
    public void IsOddChecker_InputDynamicOddNumber_IfOddReruenTrue(int a, bool expectedResult)
    {
        Calculator calc = new Calculator();
        bool result= calc.IsOddNumber(a);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void OddRanger_InputMinAndMaxRange_ReturnValidOddNumberRange()
    {
        Calculator calc = new Calculator();
        List<int> expectedOddRange = new List<int>() { 5, 7, 9 };
        List<int> result = calc.GetOddRange(5, 10);

        Assert.Equal(expectedOddRange, result);
        Assert.Contains(7, result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
        Assert.DoesNotContain(6, result);
        Assert.Equal(result.OrderBy(u=>u), result);
    }

}
