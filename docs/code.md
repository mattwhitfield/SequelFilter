# Code usage 👩‍💻

Using SequelFilter from code is fairly simple.

To match a single object to a filter, you can use:

```
var matches = ObjectFilter.Matches(
    someObject,
    "Property > 5 AND OtherProperty == 'fred'");
```

If you want to apply the same filter to multiple objects, it is more efficient to first compile the expression and then apply it:

```
var filter = SequelFilterParse.Parse(
    "Property > 5 AND OtherProperty == 'fred'");
var matches = filter.Matches(someObject);
```

You can also filter `IEnumerable` instances like so:

```
var filteredResults = myEnumerable
    .Where("Property > 5 AND OtherProperty == 'fred'")
    .ToList();
```

## Exceptions 🤔

In the main part, SequelFilter can throw two exception types:

* `ParseException` - this is thrown when the filter clause cannot be parsed. This would be, for example, if the filter text didn't make sense.
* `InvalidOperationException` - this is thrown if there is an issue at runtime. This could happen if you tried to compare a `Guid` to a `DateTime`, for example.