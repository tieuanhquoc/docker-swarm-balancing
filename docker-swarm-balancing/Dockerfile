#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["docker-swarm-balancing/docker-swarm-balancing.csproj", "docker-swarm-balancing/"]
RUN dotnet restore "docker-swarm-balancing/docker-swarm-balancing.csproj"
COPY . .
WORKDIR "/src/docker-swarm-balancing"
RUN dotnet build "docker-swarm-balancing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "docker-swarm-balancing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "docker-swarm-balancing.dll"]