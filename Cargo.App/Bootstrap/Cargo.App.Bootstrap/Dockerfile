FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bootstrap/Cargo.App.Bootstrap/Cargo.App.Bootstrap.csproj", "Bootstrap/Cargo.App.Bootstrap/"]
RUN dotnet restore "Bootstrap/Cargo.App.Bootstrap/Cargo.App.Bootstrap.csproj"
COPY . .
WORKDIR "/src/Bootstrap/Cargo.App.Bootstrap"
RUN dotnet build "Cargo.App.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cargo.App.Bootstrap.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cargo.App.Bootstrap.dll"]
