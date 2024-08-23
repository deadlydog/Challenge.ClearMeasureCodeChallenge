using FluentAssertions;

namespace CustomerItems.Tests;

public class ModulusItemMapperTests
{
	public class WhenUsingDefaultMappings()
	{
		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-101)]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(15)]
		[InlineData(12345)]
		[InlineData(int.MaxValue)]
		public void MappingAnItem_WhenTheNumberIsPositiveOrNegative_ShouldReturnTheNumber(int number)
		{
			var sut = new ModulusItemMapper();

			var result = sut.MapItem(number);

			result.Should().Be(number.ToString());
		}
	}

	public class WhenUsingDanielSchroederMappings()
	{
		private List<ItemMapping> DanielSchroederMappings = new()
		{
			new ItemMapping(3, "Daniel"),
			new ItemMapping(5, "Schroeder")
		};

		private ModulusItemMapper CreateSutWithDanielSchroederMappings()
		{
			return new ModulusItemMapper(DanielSchroederMappings);
		}

		[Fact]
		public void MappingAnItem_WhenNotDivisibleBy3Or5_ShouldReturnTheNumber()
		{
			var sut = CreateSutWithDanielSchroederMappings();
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
			var sut = CreateSutWithDanielSchroederMappings();

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
			var sut = CreateSutWithDanielSchroederMappings();

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
			var sut = CreateSutWithDanielSchroederMappings();

			var result = sut.MapItem(number);

			result.Should().Be("Daniel Schroeder");
		}
	}

	public class WhenUsingManyOrderedMappings()
	{
		private List<ItemMapping> ManyOrderedMappings = new()
		{
			new ItemMapping(3, "Three"),
			new ItemMapping(5, "Five"),
			new ItemMapping(7, "Seven"),
			new ItemMapping(9, "Nine"),
			new ItemMapping(11, "Eleven"),
			new ItemMapping(13, "Thirteen"),
		};

		private ModulusItemMapper CreateSutWithDanielSchroederMappings()
		{
			return new ModulusItemMapper(ManyOrderedMappings);
		}
	}
}
