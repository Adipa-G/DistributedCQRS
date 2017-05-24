using System.Reflection;

[assembly: AssemblyProduct("Distributed CQRS")]

#if DEBUG
 [assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyVersion("0.0.1")]
[assembly: AssemblyFileVersion("0.0.1")]