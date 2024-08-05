using Sparky_lib;

namespace Sparky_Nunit_Test;
[TestFixture]
public class CalculatorNUnitTests
{
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Set up....");
    }
    [Test]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        // Arrange
        Calculator calc = new();


        // Act
        int result = calc.AddNumbers(10, 20);

        // Assert
        Assert.That(result, Is.EqualTo(30));
    }
    [Test]
    [TestCase(5.4,10.5)]
    [TestCase(5.43,10.53)]
    [TestCase(5.49,10.59)]
    public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a, double b)
    {
        // Arrange
        Calculator calc = new();


        // Act
        double result = calc.AddNumbersDouble(a,b);

        // Assert
        Assert.That(result, Is.EqualTo(15.9d).Within(0.2d));
    }
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    [Test]
    public void IsOddChecker_InputEvenNumber_ReruenFalse() {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(10);
        Assert.That(isOdd,Is.EqualTo(false));
        Assert.IsFalse(isOdd);
    }

    [Test]
    public void IsOddChecker_InputOddNumber_ReruenTrue()
    {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(11);
        Assert.That(isOdd, Is.EqualTo(true));
        Assert.IsTrue(isOdd);
    }


    [Test]
    [TestCase(11)]
    [TestCase(13)]
    public void IsOddChecker_InputDynamicOddNumber_ReruenTrue(int a)
    {
        Calculator calc = new Calculator();
        bool isOdd = calc.IsOddNumber(a);
        Assert.That(isOdd, Is.EqualTo(true));
        Assert.IsTrue(isOdd);
    }

    [Test]
    [TestCase(10,ExpectedResult = false)]
    [TestCase(11,ExpectedResult = true)]
    public bool IsOddChecker_InputDynamicOddNumber_IfOddReruenTrue(int a)
    {
        Calculator calc = new Calculator();
        return calc.IsOddNumber(a);
        
    }

    [Test]
    public void OddRanger_InputMinAndMaxRange_ReturnValidOddNumberRange() {
        Calculator calc = new Calculator();
        List<int> expectedOddRange = new List<int>() { 5, 7, 9};
        List<int> result = calc.GetOddRange(5, 10);

        Assert.That(result, Is.EquivalentTo(expectedOddRange));

    }

}
