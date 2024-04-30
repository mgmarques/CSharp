#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GameShopping.PaymentAPI/GameShopping.PaymentAPI.csproj", "GameShopping.PaymentAPI/"]
COPY ["GameShopping.MessageBus/GameShopping.MessageBus.csproj", "GameShopping.MessageBus/"]
COPY ["GameShopping.PaymentProcessor/GameShopping.PaymentProcessor.csproj", "GameShopping.PaymentProcessor/"]
RUN dotnet restore "./GameShopping.PaymentAPI/GameShopping.PaymentAPI.csproj"
COPY . .
WORKDIR "/src/GameShopping.PaymentAPI"
RUN dotnet build "./GameShopping.PaymentAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameShopping.PaymentAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShopping.PaymentAPI.dll"]