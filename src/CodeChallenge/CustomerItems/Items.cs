using System;
using System.Collections.Generic;

namespace CustomerItems
{
	public class Items
	{
		public IEnumerable<string> GetItems(int numberOfItemsToGet)
		{
			if (numberOfItemsToGet > int.MaxValue || numberOfItemsToGet < 0)
			{
				throw new ArgumentException($"Upper bound must be between 1 and Int.MaxValue. Value passed in was '{numberOfItemsToGet}'.");
			}

			var items = new List<string>();
			for (int index = 1; index <= numberOfItemsToGet; index++)
			{
				if (index < 0)
				{
					break;
				}

				items.Add(ItemMapper.GetItemTranslatedValue(index));
			}
			return items;
		}
	}
}
