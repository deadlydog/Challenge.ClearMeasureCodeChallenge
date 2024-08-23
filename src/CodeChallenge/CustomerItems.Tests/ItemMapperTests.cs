using FluentAssertions;

namespace CustomerItems.Tests;

public class ItemMapperTests
{
	[Fact]
	public void TranslatingAnItem_WhenNotDivisibleBy3Or5_ShouldReturnTheNumber()
	{
		var input = 1;

		var result = ItemMapper.GetItemTranslatedValue(input);

		result.Should().Be("1");
	}

	[Fact]
	public void TranslatingAnItem_WhenDivisibleBy3_ShouldReturnDaniel()
	{
		var input = 3;

		var result = ItemMapper.GetItemTranslatedValue(input);

		result.Should().Be("Daniel");
	}

	[Fact]
	public void TranslatingAnItem_WhenDivisibleBy5_ShouldReturnSchroeder()
	{
		var input = 5;

		var result = ItemMapper.GetItemTranslatedValue(input);

		result.Should().Be("Schroeder");
	}

	[Fact]
	public void TranslatingAnItem_WhenDivisibleBy3And5_ShouldReturnDanielSchroeder()
	{
		var input = 15;

		var result = ItemMapper.GetItemTranslatedValue(input);

		result.Should().Be("Daniel Schroeder");
	}

	[Fact]
	public void TranslatingAnItem_WhenItsLarge_ItDoesNotCrash()
	{
		var input = int.MaxValue;

		var result = ItemMapper.GetItemTranslatedValue(input);

		result.Should().Be(int.MaxValue.ToString());
	}
}
