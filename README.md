# SequelFilter

# Introduction 👀
Sequel Filter is a C# library that allows for filtering using a SQL-like syntax. The idea is to be able to provide end-users with completely customizable filtering without having to allow them to provide their own code or write your own filtering logic.

## Installation 💾

Sequel Filter is available as a [nuget package](https://www.nuget.org/packages/SequelFilter). You can also install the package using the .net CLI:

```
dotnet add package SequelFilter
```

## Library Features 📚

* Simple injection-free filtering logic
* Support for matching single objects or filtering `IEnumerable`s
* Lightweight dependencies (only Irony parser required)

## Grammar Features ✍

* Basic comparisons with support for loose type matching
* Combinational logic with `AND` / `OR`
* Support for `IN`, `LIKE`, `BETWEEN` and `IS NULL` SQL operators
* Support for searching child enumerables with `HAS_NONE`, `HAS_ANY`, `HAS_SINGLE`

# Documentation 📖

The best way to view the documentation is to visit the [documentation on GitHub pages](https://mattwhitfield.github.io/SequelFilter/). Alternatively, you can view the documentation direct from the repository using the links to sections below.

## Documentation Sections

* [Introduction](docs/index.md) - introduction to the sequel filter library.
* [Grammar](docs/grammar.md) - covers the grammar that you can use to filter objects with some examples.
* [Code](docs/code.md) - how to integrate Sequel Filter into your project to allow filtering of objects.
* [Contributing](docs/contributing.md) - covers useful information for anyone who wants to contribute to the project.

# Contributing ✋

Any contributions are welcome. Code. Feedback on what you like or what could be better. Please feel free to fork the repo and make changes, the license is MIT so you're pretty much free to do whatever you like. For more information, please see the [contributing section](docs/contributing.md).

# Support 🤝

If you'd like to support the project but you don't want to contribute code - a really good way to help is spread the word. There isn't a donation or financial option because honestly I don't need donations. I am, however, rubbish at 'marketing'. So any help there would be greatly appreciated! Whether it's a blog post or just telling some folks at the office - every little helps.
