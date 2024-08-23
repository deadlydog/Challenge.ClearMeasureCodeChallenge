using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerItems
{
	public static class ItemMapper
	{
		public static string GetItemTranslatedValue(int number)
		{
			string value = string.Empty;
			if (number % 3 == 0)
			{
				value += "Daniel";
			}

			if (number % 5 == 0)
			{
				value += " Schroeder";
			}

			if (string.Equals(value, string.Empty, StringComparison.OrdinalIgnoreCase))
			{
				value = number.ToString();
			}

			return value.Trim();
		}
	}
}
