# Repro for [dotnet/efcore#35568](https://github.com/dotnet/efcore/issues/35568)

- [ConsoleApp.csproj](./ConsoleApp/ConsoleApp.csproj) uses EF Core 9 by default. Run the
  project to notice that queries 2, 3 & 4 fail.

- Change the project to use EF Core 8, re-run to notice that that the same queries fail in this
  version as well.

- Change to use EF Core 7, re-run to notice that all queries work.

- [Logs](./Logs/) attached.
