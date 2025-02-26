# HealthChecks

This example show how Health Checks can be used in an ASP.NET app.
In this context, HealthChecks are endpoints that can be called to see the health of the application.

This example contains one [ASP.NET app](./WebApplication1/) that hosts the default WeatherForecast example (with some modifications).
On top of this application, HealthChecks were added to demonstrate.

## Files of Interest

- [Launch Settings](./WebApplication1/Properties/launchSettings.json)
- [Health Checks Configuration](./WebApplication1/Configuration/HealthChecks.cs)
- [Endpoints](./WebApplication1/Endpoints/)
- [Health Checks](./WebApplication1/HealthChecks/Checks/)
  - [Always Healthy](./WebApplication1/HealthChecks/Checks/AlwaysHealthyHealthCheck.cs)
  - [Initialized](./WebApplication1/HealthChecks/Checks/InitializationHealthCheck.cs)
  - [Http](./WebApplication1/HealthChecks/Checks/HttpRequestHealthCheck.cs)
  - [Ping](./WebApplication1/HealthChecks/Checks/PingableHealthCheck.cs)
- [Health Check Response Writers](./WebApplication1/HealthChecks/ResponseWriters.cs)
  - [Json](./WebApplication1/HealthChecks/ResponseWriters.Json.cs)
  - [Ready](./WebApplication1/HealthChecks/ResponseWriters.Ready.cs)
  - [Live](./WebApplication1/HealthChecks/ResponseWriters.Live.cs)
- [Dockerfile](./WebApplication1/Dockerfile)
- [Project File](./WebApplication1/WebApplication1.csproj)

## Urls
- Application Endpoints:
  - [WeatherForecast](http://localhost:5080/WeatherForecast)
- HealthCheck Endpoints:
  - [Live](http://localhost:5080/health/live)
  - [Ready](http://localhost:5080/health/ready)
  - [Explain (Json)](http://localhost:5080/health/explain)
  - [All](http://localhost:5080/health/all)

## References
- [Health checks in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
- [Container Tools build properties](https://learn.microsoft.com/en-us/visualstudio/containers/container-msbuild-properties)
- [Dockerfile HEALTHCHECK reference](https://docs.docker.com/reference/dockerfile/#healthcheck)