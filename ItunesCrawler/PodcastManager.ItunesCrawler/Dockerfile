FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ItunesCrawler/PodcastManager.ItunesCrawler/PodcastManager.ItunesCrawler.csproj", "PodcastManager.ItunesCrawler/"]
RUN dotnet restore "ItunesCrawler/PodcastManager.ItunesCrawler/PodcastManager.ItunesCrawler.csproj"
COPY . .
WORKDIR "/src/PodcastManager.ItunesCrawler"
RUN dotnet build "PodcastManager.ItunesCrawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PodcastManager.ItunesCrawler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PodcastManager.ItunesCrawler.dll"]
