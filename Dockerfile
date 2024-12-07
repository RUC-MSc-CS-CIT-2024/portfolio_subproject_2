FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY ./src .

ARG TARGETARCH

RUN dotnet restore -a $TARGETARCH

WORKDIR /source/CitMovie.Api
RUN dotnet publish --no-self-contained -a $TARGETARCH --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "CitMovie.Api.dll"]
