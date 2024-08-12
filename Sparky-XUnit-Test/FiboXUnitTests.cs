using System;
using Sparky_lib;

namespace Sparky_XUnit_Test
{

	public class FiboNUnitTests
	{
		[Fact]
		public void FiboChecker_Input1_ReturnsFiboSeries()
		{
			List<int> expectedRange = new List<int>() { 0};

			Fibo fibo = new Fibo();
			fibo.Range = 1;
			List<int> result = fibo.GetFiboSeries();

			Assert.NotEmpty(result);
			Assert.Equal(expectedRange.OrderBy(x=>x), result);
			Assert.True(result.SequenceEqual(expectedRange));

		}
		[Fact]
		public void FiboChecker_Input6_ReturnsFiboSeries()
		{
			List<int> expectedRange = new List<int>() { 0,1,1,2,3,5};

			Fibo fibo = new Fibo();
			fibo.Range = 6;
			List<int> result = fibo.GetFiboSeries();

			Assert.Contains(3, result);
			Assert.Equal(6, result.Count);
			Assert.DoesNotContain(4, result);
			Assert.Equal(expectedRange, result);

		}
	}
}

