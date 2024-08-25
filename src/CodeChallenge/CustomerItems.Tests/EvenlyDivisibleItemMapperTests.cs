using FluentAssertions;

namespace CustomerItems.Tests;

public class EvenlyDivisibleItemMapperTests
{
	public class WhenMappingsAreNotProvided()
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
		public void MappingAnItem_WhenTheInputIsPositiveOrNegative_ShouldReturnTheInputNumber(int number)
		{
			var sut = new EvenlyDivisibleItemMapper();

			var result = sut.MapItem(number);

			result.Should().Be(number.ToString());
		}
	}

	public class WhenMappingsAreProvided()
	{
		private readonly List<ItemMapping> MappingsSingleNumber3 =
		[
			new ItemMapping(3, "Three"),
		];

		private readonly List<ItemMapping> MappingsAscending3579 =
		[
			new ItemMapping(3, "Three"),
			new ItemMapping(5, "Five"),
			new ItemMapping(7, "Seven"),
			new ItemMapping(9, "Nine"),
		];

		private readonly List<ItemMapping> MappingsOutOfOrder7395 =
		[
			new ItemMapping(7, "Seven"),
			new ItemMapping(3, "Three"),
			new ItemMapping(9, "Nine"),
			new ItemMapping(5, "Five"),
		];

		private readonly List<ItemMapping> MappingsDuplicates7393 =
		[
			new ItemMapping(7, "Seven"),
			new ItemMapping(3, "Three"),
			new ItemMapping(9, "Nine"),
			new ItemMapping(3, "Three"),
		];

		private EvenlyDivisibleItemMapper CreateSut(List<ItemMapping> mappings)
		{
			return new EvenlyDivisibleItemMapper(mappings);
		}

		[Fact]
		public void MappingAnItem_WhenNotDivisibleByAnyNumber_ShouldReturnTheInputNumber()
		{
			var sut = CreateSut(MappingsSingleNumber3);
			var input = 1;

			var result = sut.MapItem(input);

			result.Should().Be("1");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByOnlyOneNumber_ShouldReturnOnlyThatNumbersMapping()
		{
			var sut = CreateSut(MappingsAscending3579);
			var input = 5;

			var result = sut.MapItem(input);

			result.Should().Be("Five");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByTwoNumbers_ShouldReturnBothMappingsInOrder()
		{
			var sut = CreateSut(MappingsAscending3579);
			var input = 9;

			var result = sut.MapItem(input);

			result.Should().Be("Three Nine");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByThreeNumbers_ShouldReturnAllThreeMappingsInOrder()
		{
			var sut = CreateSut(MappingsAscending3579);
			var input = 63; // 7 * 9 = 63 (divisible by 3 too)

			var result = sut.MapItem(input);

			result.Should().Be("Three Seven Nine");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByMultipleNumbersOutOfOrder_ShouldReturnAllMappingsInSameOrder()
		{
			var sut = CreateSut(MappingsOutOfOrder7395);
			var input = 945; // 7 * 3 * 9 * 5 = 945 (divisible by all numbers)

			var result = sut.MapItem(input);

			result.Should().Be("Seven Three Nine Five");
		}

		[Fact]
		public void MappingAnItem_WhenDivisibleByMultipleNumbersWithDuplicateMappings_ShouldReturnAllMappingsInOrderWithDuplicates()
		{
			var sut = CreateSut(MappingsDuplicates7393);
			var input = 63; // 7 * 9 = 189 (divisible by 3 too)

			var result = sut.MapItem(input);

			result.Should().Be("Seven Three Nine Three");
		}
	}

	// These "Daniel Schroeder" tests could be removed, but since they were a requirement in the original challenge, I've kept them.
	public class WhenUsingDanielSchroederMappings()
	{
		private readonly List<ItemMapping> DanielSchroederMappings = new()
		{
			new ItemMapping(3, "Daniel"),
			new ItemMapping(5, "Schroeder")
		};

		private EvenlyDivisibleItemMapper CreateSutWithMappings3Daniel5Schroeder()
		{
			return new EvenlyDivisibleItemMapper(DanielSchroederMappings);
		}

		[Fact]
		public void MappingAnItem_WhenNotDivisibleBy3Or5_ShouldReturnTheNumber()
		{
			var sut = CreateSutWithMappings3Daniel5Schroeder();
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
			var sut = CreateSutWithMappings3Daniel5Schroeder();

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
			var sut = CreateSutWithMappings3Daniel5Schroeder();

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
			var sut = CreateSutWithMappings3Daniel5Schroeder();

			var result = sut.MapItem(number);

			result.Should().Be("Daniel Schroeder");
		}
	}
}
