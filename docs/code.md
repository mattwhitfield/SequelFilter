# Code usage 👩‍💻

Coming soon. Code usage examples.

In the reasonably likely event that I forget to write this page, you can do:

`ObjectFilter.Matches(someObject, "Property > 5 AND OtherProperty == 'fred'");`

You can also filter `IEnumerables` like so:

`var filteredResults = myEnumerable.Where("Property > 5 AND OtherProperty == 'fred'").ToList();`