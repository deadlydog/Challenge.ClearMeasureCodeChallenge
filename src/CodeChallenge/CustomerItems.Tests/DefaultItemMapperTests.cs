using FluentAssertions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerItems.Tests;

public class DefaultItemMapperTests
{
	[Fact]
	public void MappingAnItem_WhenNotDivisibleBy3Or5_ShouldReturnTheNumber()
	{
		var sut = new DefaultItemMapper();
		var input = 1;

		var result = sut.MapItem(input);

		result.Should().Be("1");
	}

	[Theory]
	[InlineData(3)]
	[InlineData(6)]
	[InlineData(9)]
	[InlineData(12)]
	public void MappingAnItem_WhenDivisibleBy3AndNot5_ShouldReturnDaniel(int number)
	{
		var sut = new DefaultItemMapper();

		var result = sut.MapItem(number);

		result.Should().Be("Daniel");
	}

	[Theory]
	[InlineData(5)]
	[InlineData(10)]
	[InlineData(20)]
	[InlineData(25)]
	public void MappingAnItem_WhenDivisibleBy5AndNot3_ShouldReturnSchroeder(int number)
	{
		var sut = new DefaultItemMapper();

		var result = sut.MapItem(number);

		result.Should().Be("Schroeder");
	}

	[Theory]
	[InlineData(15)]
	[InlineData(30)]
	[InlineData(45)]
	[InlineData(60)]
	public void MappingAnItem_WhenDivisibleBy3And5_ShouldReturnDanielSchroeder(int number)
	{
		var sut = new DefaultItemMapper();

		var result = sut.MapItem(number);

		result.Should().Be("Daniel Schroeder");
	}

	[Fact]
	public void MappingAnItem_WhenItsLarge_ItDoesNotCrash()
	{
		var sut = new DefaultItemMapper();
		var input = int.MaxValue;

		var result = sut.MapItem(input);

		result.Should().Be(int.MaxValue.ToString());
	}
}
