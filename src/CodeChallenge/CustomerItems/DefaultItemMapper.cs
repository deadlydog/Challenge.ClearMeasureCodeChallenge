using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerItems
{
	public interface IMapItems
	{
		public string MapItem(int number);
	}

	public class DefaultItemMapper : IMapItems
	{
		public string MapItem(int number)
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
