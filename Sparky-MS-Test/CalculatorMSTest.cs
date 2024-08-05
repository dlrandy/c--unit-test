using Sparky_lib;

namespace Sparky_MS_Test;

[TestClass]
public class CalculatorMSTest
{
    [TestMethod]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        // Arrange
        Calculator calc = new ();


        // Act
        int result = calc.AddNumbers(10, 20);

        // Assert
        Assert.AreEqual(30, result);
    }
}
