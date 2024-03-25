FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Sandbox.Api/Sandbox.Api.csproj", "src/Sandbox.Api/"]
RUN dotnet restore "./src/Sandbox.Api/Sandbox.Api.csproj"
COPY . .
WORKDIR "/src/src/Sandbox.Api"
RUN dotnet build "./Sandbox.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sandbox.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
LABEL "org.opencontainers.image.source"=https://github.com/davist-ir/Sandbox
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sandbox.Api.dll"]