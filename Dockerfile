FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY src/Rocket.API ./
RUN dotnet restore && \
    dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

USER $APP_UID
ENTRYPOINT ["dotnet", "Rocket.API.dll"]