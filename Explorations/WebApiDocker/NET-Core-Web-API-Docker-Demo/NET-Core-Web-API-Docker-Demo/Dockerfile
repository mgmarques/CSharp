#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["NET-Core-Web-API-Docker-Demo/NET-Core-Web-API-Docker-Demo.csproj", "NET-Core-Web-API-Docker-Demo/"]
RUN dotnet restore "NET-Core-Web-API-Docker-Demo/NET-Core-Web-API-Docker-Demo.csproj"
COPY . .
WORKDIR "/src/NET-Core-Web-API-Docker-Demo"
RUN dotnet build "NET-Core-Web-API-Docker-Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NET-Core-Web-API-Docker-Demo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NET-Core-Web-API-Docker-Demo.dll"]
