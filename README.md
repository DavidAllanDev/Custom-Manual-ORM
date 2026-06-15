# Custom-Manual-ORM
A simple example of a custom manual ORM.

Status
------
This repository was migrated from netstandard2.1/netcore3.1 to .NET 10 (net10.0).

Migration summary
-----------------
- All projects now target net10.0.
- Test packages updated: Microsoft.NET.Test.Sdk (17.10.0), MSTest.* (3.0.2).
- Microsoft.Data.SqlClient updated to 7.0.1.
- global.json added to pin the SDK to 10.0.301 (rollForward: disable).

Requirements
------------
- .NET SDK 10.0.301 installed in development/CI environments.

Build and test
--------------
- dotnet build
- dotnet test

Notes
-----
- .NET 10 is LTS. If you need compatibility with consumers that require netstandard, consider multi-targeting.
- A branch "targeting net8.0" was created during the process; master now targets net10.0.

Project structure
-----------------
Custom.Manual.ORM.Base
  - Abstractions for entity managers and SQL base classes used to access a database (SQL Server supported).

Custom.Manual.ORM.Data
  - Database connection and settings used by the Base project.

Custom.Manual.ORM.Domain
  - Domain project where you model entities used by the ORM.

Custom.Manual.ORM.Data.Repositories
  - Aggregates the other projects and provides repository implementations.

Custom.Manual.ORM.Cache
  - Cache project (work in progress).

Compatibility
-------------
- OS: Windows and macOS
- Visual Studio: recent versions that support .NET 10 (update Visual Studio/VS for Mac if needed)

Next steps
----------
- Consider adding multi-targeting if you need to support netstandard consumers.
- Update CI pipelines to use the pinned .NET SDK or install the required SDK on runners.

If you want, I can commit this README update and push the changes to the remote.
