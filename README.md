# Dotnet Native Exports (DNNE) Example

Simple example showing a .NET `.dll` which gets loaded by a third-party C/C++ application, and passed a struct containing function pointers.
The struct has a function pointer `->GetFunc(const char* name)` which can be used to "look up" other functions in the C/C++ namespace and return function pointers to them.

Showcases basic string management and marshalling structs + working with delegates/function pointers.
