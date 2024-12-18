FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
RUN useradd -m appuser
USER appuser
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["WebApplication3/WebApplication3.csproj", "WebApplication3/"]
RUN dotnet restore "./WebApplication3/WebApplication3.csproj"

COPY . . 
WORKDIR "/src/WebApplication3"
RUN dotnet build "./WebApplication3.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApplication3.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebApplication3.dll"]
