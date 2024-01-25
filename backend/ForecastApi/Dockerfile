FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ForecastApi/ForecastApi.csproj", "ForecastApi/"]
COPY ["ForecastApi.ExternalServices/ForecastApi.ExternalServices.csproj", "ForecastApi.ExternalServices/"]
COPY ["ForecastApi.Application/ForecastApi.Application.csproj", "ForecastApi.Application/"]
RUN dotnet restore "./ForecastApi/./ForecastApi.csproj"
COPY . .
WORKDIR "/src/ForecastApi"
RUN dotnet build "./ForecastApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ForecastApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForecastApi.dll"]