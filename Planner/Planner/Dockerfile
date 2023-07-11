FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Planner/Planner.csproj", "Planner/"]
RUN dotnet restore "Planner/Planner.csproj"
COPY . .
WORKDIR "/src/Planner"
RUN dotnet build "Planner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Planner.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Planner.dll"]
