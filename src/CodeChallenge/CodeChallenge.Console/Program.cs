using CustomerItems;

var app = new ItemsRetriever();
var items = app.GetItems(int.MaxValue);

foreach (string item in items)
{
	Console.WriteLine(item);
}
