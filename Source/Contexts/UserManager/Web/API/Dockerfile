#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

RUN apt-get update && apt-get install -y curl
RUN apt-get update && apt-get install -y wget

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Contexts/UserManager/Web/API/Adventuring.Contexts.UserManager.Web.API.csproj", "Source/Contexts/UserManager/Web/API/"]
RUN dotnet restore "Source/Contexts/UserManager/Web/API/Adventuring.Contexts.UserManager.Web.API.csproj"
COPY . .
WORKDIR "/src/Source/Contexts/UserManager/Web/API"
RUN dotnet build "Adventuring.Contexts.UserManager.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adventuring.Contexts.UserManager.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adventuring.Contexts.UserManager.Web.API.dll"]