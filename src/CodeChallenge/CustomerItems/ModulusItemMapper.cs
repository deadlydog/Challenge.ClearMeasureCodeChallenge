using System.Text;

namespace CustomerItems
{
	public record ItemMapping(int Number, string Value);

	public class ModulusItemMapper : IMapItems
	{
		private IEnumerable<ItemMapping> _itemMappings = Enumerable.Empty<ItemMapping>();

		public ModulusItemMapper()
			: this(Enumerable.Empty<ItemMapping>())
		{ }

		public ModulusItemMapper(IEnumerable<ItemMapping> itemMappings)
		{
			_itemMappings = itemMappings ?? throw new ArgumentNullException(nameof(itemMappings));
		}

		public string MapItem(int number)
		{
			StringBuilder value = new();

			foreach (var itemMapping in _itemMappings)
			{
				if (number % itemMapping.Number == 0)
				{
					value.AppendFormat("{0} ", itemMapping.Value);
				}
			}

			if (value.Length == 0)
			{
				value.Append(number.ToString());
			}

			return value.ToString().Trim();
		}
	}
}
