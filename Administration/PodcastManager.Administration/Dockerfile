FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Administration/PodcastManager.Administration/PodcastManager.Administration.csproj", "PodcastManager.Administration/"]
RUN dotnet restore "Administration/PodcastManager.Administration/PodcastManager.Administration.csproj"
COPY . .
WORKDIR "/src/PodcastManager.Administration"
RUN dotnet build "PodcastManager.Administration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PodcastManager.Administration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PodcastManager.Administration.dll"]
