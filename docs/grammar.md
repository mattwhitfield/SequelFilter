# Filtering grammar ✍

The filtering grammar is designed to be SQL-like. You can reference fields or properties of objects in a C# style and use familiar dotted access.

Let's start with an example. Considering this simple object:

```
public class World
{
    public IEnumerable<Country> Countries => new[]
    { 
        Country.US,
        Country.UK,
        Country.France
    };

    public RandomStatistics Statistics { get; }
        = new RandomStatistics();
}

public class RandomStatistics
{
    public long Population => 8007821437;

    public long Websites => 1630322759;

    public long FieldOfDreams = 201;
}
```

Given the object `World` the filter `Statistics.FieldOfDreams == 201` would return true. So would the filter `Statistics.Population > 1000000`. As would the filter `Statistics.FieldOfDreams == 201 AND Statistics.Population > 1000000`.

## Language elements 🧩

The following language elements are available:

| Name | Function |
| - | - |
| Comparison | Allows values to be compared to eachother. Operators are `=`, `==`, `!=`, `<>`, `>`, `>=`, `<`, `<=`. |
| Binary | Allows two results to be combined using `AND` or `OR` (or `&&` or `||`). More complex structures can be facilitated through the use of parentheses. |
| BETWEEN | Allows checking whether a value is between a lower and upper bound. Can also be stated as NOT BETWEEN. |
| IN | Allows checking whether a value is in a specified list of values. |
| NOT | Allows an expression to be negated. |
| Field Reference | Access to a member with dotted syntax (e.g. `Statistics.Population`). |
| Enumerable | Allows running a comparison against the elements in an `IEnumerable` - with HAS_ANY, HAS_SINGLE and HAS_NONE being available |
| IS NULL | Allows checking if a value is null. Can also be stated as IS NOT NULL. |
| LIKE | Allows checking if a value matches a SQL LIKE pattern. Patterns can include character ranges `[a-z]`, single character wildcards `?` and multiple character wildcards `%`. Can also be stated as NOT LIKE. |

Some good examples of usage can be found in the [unit tests](https://github.com/mattwhitfield/SequelFilter/blob/main/src/SequelFilter.Tests/IEnumerableExtensionsTests.cs).

## Combining language elements 🚧

You can combine any of the individual language elements to create more complex filters. Here are some examples:

`Statistics.FieldOfDreams == 201 AND Statistics.Population > 1000000`

`Statistics.FieldOfDreams == 201 OR (Statistics.Population > 1000000 AND Statistics.Websites > 5000)`

`Statistics.FieldOfDreams == 201 OR Statistics.Population > 1000000 OR Statistics.Websites > 5000`