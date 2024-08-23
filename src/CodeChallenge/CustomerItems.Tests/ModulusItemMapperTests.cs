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

	public class WhenUsingManyAscendingOrderedMappings()
	{
		private List<ItemMapping> AscendingMappings = new()
		{
			new ItemMapping(3, "Three"),
			new ItemMapping(5, "Five"),
			new ItemMapping(7, "Seven"),
			new ItemMapping(9, "Nine"),
		};

		private ModulusItemMapper CreateSutWithAscendingMappings()
		{
			return new ModulusItemMapper(AscendingMappings);
		}

		[Fact]
		public void MappingAnItem_WhenNotDivisibleByAnyNumber_ShouldReturnTheNumber()
		{
			var sut = CreateSutWithAscendingMappings();
			var input = 1;

			var result = sut.MapItem(input);

			result.Should().Be("1");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleBy3_ShouldReturnThree()
		{
			var sut = CreateSutWithAscendingMappings();
			var input = 3;

			var result = sut.MapItem(input);

			result.Should().Be("Three");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleBy5_ShouldReturnFive()
		{
			var sut = CreateSutWithAscendingMappings();
			var input = 5;

			var result = sut.MapItem(input);

			result.Should().Be("Five");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleBy7_ShouldReturnSeven()
		{
			var sut = CreateSutWithAscendingMappings();
			var input = 7;

			var result = sut.MapItem(input);

			result.Should().Be("Seven");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleBy9_ShouldReturnThreeAndNine()
		{
			var sut = CreateSutWithAscendingMappings();
			var input = 9;

			var result = sut.MapItem(input);

			result.Should().Be("Three Nine");
		}
	}

	public class WhenUsingManyDescendingOrderedMappings()
	{
		private List<ItemMapping> DescendingMappings = new()
		{
			new ItemMapping(9, "Nine"),
			new ItemMapping(7, "Seven"),
			new ItemMapping(5, "Five"),
			new ItemMapping(3, "Three"),
		};

		private ModulusItemMapper CreateSutWithDescendingMappings()
		{
			return new ModulusItemMapper(DescendingMappings);
		}

		[Fact]
		public void MappingAnItem_WhenNotDivisibleByAnyNumber_ShouldReturnTheNumber()
		{
			var sut = CreateSutWithDescendingMappings();
			var input = 1;

			var result = sut.MapItem(input);

			result.Should().Be("1");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByOnlyOneNumber_ShouldReturnThatNumbersMapping()
		{
			var sut = CreateSutWithDescendingMappings();
			var input = 3;

			var result = sut.MapItem(input);

			result.Should().Be("Three");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByTwoNumbers_ShouldReturnBothMappingsInOrder()
		{
			var sut = CreateSutWithDescendingMappings();
			var input = 9;

			var result = sut.MapItem(input);

			result.Should().Be("Nine Three");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByThreeNumbers_ShouldReturnAllMappingsInOrder()
		{
			var sut = CreateSutWithDescendingMappings();
			var input = 63;

			var result = sut.MapItem(input);

			result.Should().Be("Nine Seven Three");
		}
	}

	public class WhenUsingManyOutOfOrderMappings()
	{
		private List<ItemMapping> DescendingMappings = new()
		{
			new ItemMapping(7, "Seven"),
			new ItemMapping(3, "Three"),
			new ItemMapping(9, "Nine"),
			new ItemMapping(5, "Five"),
		};

		private ModulusItemMapper CreateSutWithDescendingMappings()
		{
			return new ModulusItemMapper(DescendingMappings);
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByMultipleNumbers_ShouldReturnAllMappingsInOrder()
		{
			var sut = CreateSutWithDescendingMappings();
			var input = 945; // 7 * 3 * 9 * 5 = 945

			var result = sut.MapItem(input);

			result.Should().Be("Seven Three Nine Five");
		}
	}
}
