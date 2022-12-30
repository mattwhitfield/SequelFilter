# Contributing ✋

Sequel Filter isn't affiliated with any company, so the development is completely open source. As such, the development isn't guaranteed to be on any particular schedule. Any input from the community is hugely valuable, even if it's just raising an issue or suggesting a feature. You can get started by visiting the [SequelFilter GitHub repo](https://github.com/mattwhitfield/SequelFilter).

If you want to go further, and contribute some code to the project, then this guide aims to give an indication of how the Sequel Filter library is engineered and explain some of the decisions.

## Guidelines 🚧

Please follow these guidelines:

* Smaller PRs that cover a single functionality enhancement are generally preferred
* Please ensure that there are no Code Analysis warnings or test failures created by the code

I am really excited to see what you come up with to take this project forward - so thank you again!

## Solution Layout 🗃

The solution is fairly simple, consisting of just the library and it's tests.

In the main project, the Irony based grammary and main entry points are in the root folder.

The 'Comparison' folder contains the code that facilitates loosely-typed value comparisons.

The 'NodeTransforms' folder contains the code responsible for converting parsed syntax trees into executable delegates.

The 'Resolvers' folder contains the various resolvers that allow field references to be resolved from input object.
