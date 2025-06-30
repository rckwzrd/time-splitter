# Time Splitter

Simple C# dotnet console app for printing current time in CDT, CST, and UTC. This project was used to satisfy the "First Personal Project" course on [Boot.Dev](https://www.boot.dev/).

Why central time and coordinated universal time? I am universally bad at splitting the difference between central and UTC time when looking at logs. This tool emits a chunk of json with the current time in UTC and CST or DST with the hope that it will help make better relative comparisons.

Why json? I want this console app to loosely follow the unix philosophy of simple and composable tools. Emitting a small blob of serialized json to standard out strikes a good balance between human readability and interoperability with other CLI tools.

Why C# and dotnet? I inhereted a C# project at work and need to skill up on building and releasing in the dotnet ecosystem. By keeping the tool simple I can focus on figuring out how to build and release for Windows and Linux.


Some learning goals:
- [x] Build and release with Github action, cut a tagged download, linux and windows
- [x] Basic unit test, confirm output is JSON like, run tests local and on GH
- [x] Understand basics of dotnet compilation, executables, and binaries


