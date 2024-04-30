#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GameShopping.OrderAPI/GameShopping.OrderAPI.csproj", "GameShopping.OrderAPI/"]
COPY ["GameShopping.MessageBus/GameShopping.MessageBus.csproj", "GameShopping.MessageBus/"]
RUN dotnet restore "./GameShopping.OrderAPI/GameShopping.OrderAPI.csproj"
COPY . .
WORKDIR "/src/GameShopping.OrderAPI"
RUN dotnet build "./GameShopping.OrderAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameShopping.OrderAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShopping.OrderAPI.dll"]