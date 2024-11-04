# Lesson 10 exercises
## Deployment, logging and testing
For this exercise, you have been provided with some application code and test code. Open the solution located at `exercises/lesson-10/Lists/Lists.sln` and make sure you are able to run both the application and the tests[^1].

## Exercise 10-01 
### Write a controller from the provided unit tests
A developer has written some unit test for `ListService` (located @ `exercises/lesson-10/Lists/src/Lists/Services/ListService.cs`).

1. Try to run `dotnet test` in `exercises/lesson-10/Lists/test/ListTests`
2. Add the business logic in `exercises/lesson-10/Lists/src/Lists/Services/ListService.cs` to make the tests green!

## Exercise 10-02
### Logging with Serilog
Setup logging with Serilog.

1. Add the following NuGet packages to the project:
    - `Serilog.AspNetCore`
    - `Serilog.Sinks.Console`
    - `Serilog.Sinks.Seq`
2. Configure Serilog in `Program.cs` (see `examples/lesson-10-deployment-logging-and-tests/logging/Program.cs` for inspiration and don't forget to check out `examples/lesson-10-deployment-logging-and-tests/logging/appsettings.Development.json` as well)
3. Create a container from the Seq Docker image, you can use `examples/lesson-10-deployment-logging-and-tests/logging/docker-compose.yml` to get started.
4. Inject an `ILogger` instance in `ListController` and log data from the endpoints.
5. Open Seq and review the structured logging. Why is it smart?

[^1]: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test