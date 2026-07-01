# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/CleanArchitectureMVC

COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["NuGet.config", "."]
COPY ["local-packages/", "local-packages/"]
COPY ["CleanArchitectureMVC.csproj", "."]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet restore "CleanArchitectureMVC.csproj" --configfile NuGet.config

COPY . .

RUN dotnet publish "CleanArchitectureMVC.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    -p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ricardo.CleanArchitectureMVC.dll"]
