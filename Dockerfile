FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY ./src .

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "CitMovie.Api.dll"]
