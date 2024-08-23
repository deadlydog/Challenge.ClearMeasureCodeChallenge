using CustomerItems;

List<ItemMapping> DanielSchroederMappings = new()
{
	new ItemMapping(3, "Daniel"),
	new ItemMapping(5, "Schroeder")
};

var app = new ItemsRetriever(new ModulusItemMapper(DanielSchroederMappings));
var items = app.GetItems(int.MaxValue);

foreach (string item in items)
{
	Console.WriteLine(item);
}
