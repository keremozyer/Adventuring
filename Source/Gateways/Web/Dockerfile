#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

RUN apt-get update && apt-get install -y curl jq

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Gateways/Adventuring.Gateways.Web/Adventuring.Gateways.Web.csproj", "Source/Gateways/Adventuring.Gateways.Web/"]
RUN dotnet restore "Source/Gateways/Adventuring.Gateways.Web/Adventuring.Gateways.Web.csproj"
COPY . .
WORKDIR "/src/Source/Gateways/Adventuring.Gateways.Web"
RUN dotnet build "Adventuring.Gateways.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adventuring.Gateways.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adventuring.Gateways.Web.dll"]