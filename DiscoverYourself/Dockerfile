FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5137

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DiscoverYourself/DiscoverYourself.csproj", "DiscoverYourself/"]
RUN dotnet restore "DiscoverYourself/DiscoverYourself.csproj"
COPY . .
WORKDIR "/src/DiscoverYourself"
RUN dotnet build "DiscoverYourself.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DiscoverYourself.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscoverYourself.dll"]
