# Clear Measure Code Challenge

## 💬 Description

This is my solution to the Clear Measure Code Challenge.

The challenge is to create a NuGet package for others to consume that:

- Returns back a list of strings based on the number of items specified, where the string corresponds to the item's index.
- Allows the consumer to specify a list of number-string pairs, where if the index is divisible by the number, the string is returned instead of the index.
- If the index is divisible by more than one number, the strings are concatenated together in the order they were provided.

We also want to create a build script that can be used to build the NuGet package and run tests locally.

## 🚀 Quick start

To view the code, open the [CodeChallenge.sln](src/CodeChallenge/CodeChallenge.sln) solution in Visual Studio.
Assuming the `CodeChallenge.Console` project is set as the startup project, you can run the app to see the sample output produced by the [Program.cs file](src/CodeChallenge/CodeChallenge.Console/Program.cs).

### Using the NuGet library

Once you have the NuGet package installed on your project, you can use the default `EvenlyDivisibleItemMapper` class to map items, like this:

```csharp
List<ItemMapping> mappings = new()
{
  new ItemMapping(3, "DivisibleBy3"),
  new ItemMapping(5, "DivisibleBy5"),
  new ItemMapping(10, "DivisibleBy10"),
};

var itemMapper = new EvenlyDivisibleItemMapper(mappings)

var app = new ItemsRetriever(itemMapper);
var items = app.GetItems(100);
```

`items` will contain a list of 100 strings, where the string is the item number if it is not evenly divisible by any of the numbers in the mappings, or the concatenated strings of the mappings if it is.

e.g. Sample output:

```text
1
2
DivisibleBy3
4
DivisibleBy5
DivisibleBy3
7
8
DivisibleBy3
DivisibleBy5 DivisibleBy10
11
...
```

### Providing your own item mapper

If you do not want items returned based on if the index is evenly divisible by a number, you can create your own item mapper by implementing the `IMapItems` interface.

Here is an example of an item mapper that returns the letter of the alphabet that corresponds to the number provided:

```csharp
public class AlphabetItemMapper : IMapItems
{
  public string MapItem(int number)
  {
    // Return the letter of the alphabet that corresponds to the number passed in.
    return ((char)((number % 26) + 64)).ToString();
  }
}
```

You can then use this item mapper in the `ItemsRetriever` class like this:

```csharp
var alphabetItemRetriever = new ItemsRetriever(new AlphabetItemMapper());
var letters = alphabetItemRetriever.GetItems(100);
```

## ✋ How to contribute

See [the Contributing page](docs/Contributing.md) for details on building and running the application.

## ⏪ Changelog

See what's changed in the application over time by viewing [the changelog](Changelog.md).
