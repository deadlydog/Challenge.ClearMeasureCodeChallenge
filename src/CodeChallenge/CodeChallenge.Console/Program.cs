using CustomerItems;

List<ItemMapping> DanielSchroederMappings = new()
{
	new ItemMapping(3, "Daniel"),
	new ItemMapping(5, "Schroeder")
};

var itemRetriever = new ItemsRetriever(new ModulusItemMapper(DanielSchroederMappings));
var items = itemRetriever.GetItems(100000);

foreach (var item in items)
{
	Console.WriteLine(item);
}

Console.WriteLine("Press Enter to continue and see results of the first 100 results using the AlphabetItemMapper...");
Console.ReadLine();

var alphabetItemRetriever = new ItemsRetriever(new AlphabetItemMapper());
var letters = alphabetItemRetriever.GetItems(100);

foreach (var letter in letters)
{
	Console.WriteLine(letter);
}

public class AlphabetItemMapper : IMapItems
{
	public string MapItem(int number)
	{
		// Return the letter of the alphabet that corresponds to the number passed in.
		return ((char)((number % 26) + 64)).ToString();
	}
}
