using FluentAssertions;

namespace CustomerItems.Tests;
public class ItemsRetrieverTests
{
	[Fact]
	public void GettingItems_WhenGivenANumberOfItemsToGet_ShouldReturnThatNumberOfItems()
	{
		// Arrange.
		var sut = new ItemsRetriever();
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
		var sut = new ItemsRetriever();
		int numberOfItemsToGet = -1;

		// Act.
		Action act = () => sut.GetItems(numberOfItemsToGet).ToArray();

		// Assert.
		act.Should().Throw<ArgumentException>().WithMessage("Number of items to get must be greater than zero*");
	}

	[Fact]
	public void GettingItems_WhenGivenZeroItemsToGet_ShouldThrowAnArgumentException()
	{
		// Arrange.
		var sut = new ItemsRetriever();
		int numberOfItemsToGet = 0;

		var result = sut.GetItems(numberOfItemsToGet);

		// Act.
		Action act = () => sut.GetItems(numberOfItemsToGet).ToArray();

		// Assert.
		act.Should().Throw<ArgumentException>().WithMessage("Number of items to get must be greater than zero*");
	}

	[Fact]
	public void CreatingTheInstance_WhenProvidingANullItemMapper_ShouldThrowAnArgumentNullException()
	{
		// Arrange.
		IMapItems itemMapper = null!;

		// Act.
		Action act = () => new ItemsRetriever(itemMapper);

		// Assert.
		act.Should().Throw<ArgumentNullException>();
	}
}
