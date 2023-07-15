FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bootstrap/CargoApp.Bootstrap/CargoApp.Bootstrap.csproj", "Bootstrap/CargoApp.Bootstrap/"]
RUN dotnet restore "Bootstrap/CargoApp.Bootstrap/CargoApp.Bootstrap.csproj"
COPY . .
WORKDIR "/src/Bootstrap/CargoApp.Bootstrap"
RUN dotnet build "CargoApp.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CargoApp.Bootstrap.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CargoApp.Bootstrap.dll"]
