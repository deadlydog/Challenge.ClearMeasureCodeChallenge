using FluentAssertions;

namespace CustomerItems.Tests;
public class ItemsTests
{
	[Fact]
	public void GettingItems_WhenGivenANumberOfItemsToGet_ShouldReturnThatNumberOfItems()
	{
		// Arrange.
		var sut = new Items();
		int numberOfItemsToGet = 100;

		// Act.
		var result = sut.GetItems(numberOfItemsToGet);

		// Assert.
		result.Count().Should().Be(numberOfItemsToGet);
	}

	[Fact]
	public void GettingItems_WhenGivenANegativeNumberToGet_ShouldThrowAnArgumentException()
	{
		// Arrange.
		var sut = new Items();
		int numberOfItemsToGet = -1;

		// Act.
		Action act = () => sut.GetItems(numberOfItemsToGet);

		// Assert.
		act.Should().Throw<ArgumentException>();
	}
}
