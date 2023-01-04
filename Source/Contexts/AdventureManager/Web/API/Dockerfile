#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

RUN apt-get update && apt-get install -y curl jq

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Contexts/AdventureManager/Web/API/Adventuring.Contexts.AdventureManager.Web.API.csproj", "Source/Contexts/AdventureManager/Web/API/"]
RUN dotnet restore "Source/Contexts/AdventureManager/Web/API/Adventuring.Contexts.AdventureManager.Web.API.csproj"
COPY . .
WORKDIR "/src/Source/Contexts/AdventureManager/Web/API"
RUN dotnet build "Adventuring.Contexts.AdventureManager.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adventuring.Contexts.AdventureManager.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adventuring.Contexts.AdventureManager.Web.API.dll"]