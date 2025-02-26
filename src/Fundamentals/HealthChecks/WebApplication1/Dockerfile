# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# Install ping utility, used by Ping C# class
# REMARK: one might consider to combine the next apt install commands into 1 install command for build performance.
RUN apt update && apt install -y iputils-ping
# Installs curl for the healthchecks in this DockerFile (See HEALTHCHECK keyword):
RUN apt update && apt install -y curl
# Define the HEALTHCHECK for this Dockerfile:
# See bottom of this Dockerfile for more info.
HEALTHCHECK --start-interval=1s --start-period=60s CMD curl --fail http://localhost:8080/health/ready || exit
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
RUN dotnet restore "./WebApplication1/WebApplication1.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]


# REMARKS Regarding the HEALTHCHECK definition
# 1. Source: https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0#docker-healthcheck
# 2. See Also: https://docs.docker.com/reference/dockerfile/#healthcheck
# 3. In this example the HEALTHCHECK is set in the 'base' layer, so that debugging from visual studio is possible even in ContainerDevelopmentMode set to Fast.
# 4. The health check in this example uses the following parameters: --start-interval=1s --start-period=60s
#	 This means that during a startup period of 60 seconds, the dockerfile healthcheck is performed every 1s.
#	 This is done in this example as a way of demonstrating what happens in the HealthCheck during startup.
#	 Keep in mind that this might be an unnecessary performance strain on your container.
#	 During this startup period, UNHEALTHY results are considered due to startup, and the container will indicate status 'health: starting'
#	 After these 60s, or after a HEALTHY result, the interval goes to the regular interval (by default 30s)