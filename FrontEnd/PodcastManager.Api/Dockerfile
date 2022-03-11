FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FrontEnd/PodcastManager.Api/PodcastManager.Api.csproj", "PodcastManager.Api/"]
RUN dotnet restore "FrontEnd/PodcastManager.Api/PodcastManager.Api.csproj"
COPY . .
WORKDIR "/src/PodcastManager.Api"
RUN dotnet build "PodcastManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PodcastManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PodcastManager.Api.dll"]
