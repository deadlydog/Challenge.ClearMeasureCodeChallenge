# Contributing to this project

Feel free to open an issue or pull request.

## 🛠 Tech stack

This is a C# project that uses .NET 8 for the console app and tests, and produces a .NET Standard 2.0 NuGet package.

## 🏃‍♂️ Building and running locally

To build the project locally, run the [build.cmd](/build.cmd) script in the root of the repository.
This will call the [Build-Solution.ps1](/build/Build-Solution.ps1) script to clean the solution and previous build artifacts, build the solution, and run the tests.
It will create a `BuildArtifacts` directory in the root of the repository that contains the published artifacts.

The NuGet package to publish will be located in the `BuildArtifacts\package\release` directory.

## 🏗 Build and deployment pipelines

Automated build and deployment pipelines that create, version, and publish the NuGet package have not been created yet.

## ⁉ Why was a specific decision made

Curious about some of the choices made in this project?
The reasons may be documented in the [Architecture Decision Records](/docs/ArchitectureDecisionRecords/).
