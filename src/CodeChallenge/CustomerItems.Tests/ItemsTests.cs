using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
