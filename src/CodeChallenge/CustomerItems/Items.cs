using System;
using System.Collections.Generic;

namespace CustomerItems
{
	public class Items
	{
		public IEnumerable<string> GetItems(int numberOfItemsToGet)
		{
			if (numberOfItemsToGet < 0)
			{
				throw new ArgumentException($"Number of items to get must be greater than zero. Value passed in was '{numberOfItemsToGet}'.");
			}

			for (int index = 1; index <= numberOfItemsToGet; index++)
			{
				yield return ItemMapper.GetItemTranslatedValue(index);
			}
		}
	}
}
