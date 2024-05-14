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
var filter = SequelFilterParser.Parse(
    "Property > 5 AND OtherProperty == 'fred'");
var matches = filter.Matches(someObject);
```

You can also filter `IEnumerable` instances like so:

```
var filteredResults = myEnumerable
    .Where("Property > 5 AND OtherProperty == 'fred'")
    .ToList();
```

## Object resolution 🔍

When you compile a filter, you receive a delegate of type `ExecutableExpression` which is defined as:

```
delegate bool ExecutableExpression(IFieldReferenceResolver fieldReferenceResolver);
```

Effectively, `IFieldReferenceResolver` is used to resolve dotted field references. So if during executing the filter needs to get the value for a dotted reference, a call will be made to the `Resolve` member of `IFieldReferenceResolver`.

There are a couple of built in `IFieldReferenceResolver` types. For the examples below we will consider the dotted field reference `first.second`:

* SingleObjectResolver - for when you want to filter against a single instance. `first` would be resolved as a property of that instance, and `second` would be resolved as a property of whatever object was returned by `first`.
* MultiObjectResolver - for when you want a set of instances that represent the top-level named objects. `first` would be used to look up an object that was passed in to the constructor of `MutliObjectResolver` and `second` would be resolved as a property of that object.
* ExtendedResolver - this is mostly for internal use, and allows you to introduce an extra named top-level object. This facilitates the `x => x.Property` style syntax of the enumerable language elements, such that each element of the enumerable can be resolved as `x` (or whatever identifier you choose) in the expression to the right of the 'goes into'.

## Field selectors 👈

Field selectors allow you to create a list of field selections - which enables things like selecting a set of fields for an export.

```
var selectors = SequelFilterParser.ParseSelectors("Property, OtherProperty");
```

The return type is `IList<FieldSelector>` - and each `FieldSelector` contains both the field name and a delegate for extracting the field value given an `IFieldReferenceResolver` instance.

## Exceptions 🤔

In the main part, SequelFilter can throw two exception types:

* `ParseException` - this is thrown when the filter clause cannot be parsed. This would be, for example, if the filter text didn't make sense.
* `InvalidOperationException` - this is thrown if there is an issue at runtime. This could happen if you tried to compare a `Guid` to a `DateTime`, for example.