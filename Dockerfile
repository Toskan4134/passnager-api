FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5106

VOLUME ["./app/logs"]

ENV ASPNETCORE_URLS=http://+:5106

ENTRYPOINT ["dotnet", "passnager-api.dll"]