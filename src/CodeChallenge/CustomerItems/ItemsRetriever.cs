namespace CustomerItems
{
	public interface IMapItems
	{
		public string MapItem(int number);
	}

	public class ItemsRetriever
	{
		public IMapItems _itemMapper;

		public ItemsRetriever()
			: this(new ModulusItemMapper())
		{ }

		public ItemsRetriever(IMapItems itemMapper)
		{
			_itemMapper = itemMapper ?? throw new ArgumentNullException(nameof(itemMapper));
		}

		public IEnumerable<string> GetItems(int numberOfItemsToGet)
		{
			if (numberOfItemsToGet < 0)
			{
				throw new ArgumentException($"Number of items to get must be greater than zero. Value passed in was '{numberOfItemsToGet}'.");
			}

			for (int index = 1; index <= numberOfItemsToGet; index++)
			{
				yield return _itemMapper.MapItem(index);
			}
		}
	}
}
