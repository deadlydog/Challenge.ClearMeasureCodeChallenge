# 001 - .NET version for NuGet package

## Status

2024-08-23 - Accepted

## Context

Our project creates a NuGet package that consumers can use in their applications.
When defining our NuGet package project, we have to choose the target framework.
We have the option to target .NET Standard (e.g. 2.0) or .NET (e.g. 8).

Typically when creating NuGet packages you want to target the lowest version of the framework that your package can support.
This allows consumers to use your package in a wider range of applications.

## Target frameworks considered

### .NET Standard 2.0

This would allow the NuGet package to be used in older .NET Framework apps, as well as .NET apps.
It does however require adding the following line of code to get it to compile:

```csharp
namespace System.Runtime.CompilerServices { internal class IsExternalInit { } }
```

and specifying the language version in the project file:

```xml
<LangVersion>latest</LangVersion>
```

### .NET 8

This allows us to use the latest language features, such as records, but would restrict the NuGet package to only be used in .NET 8+ apps.

## Decision

We will target .NET Standard 2.0 for the NuGet package, and add in the necessary code to get it to compile.
It is a small, easy, one-time cost to allow our NuGet package to have the largest reach.
