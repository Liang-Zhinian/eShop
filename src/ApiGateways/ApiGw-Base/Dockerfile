FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY src/ApiGateways/ApiGw-Base/OcelotApiGw.csproj src/ApiGateways/ApiGw-Base/
RUN dotnet restore src/ApiGateways/ApiGw-Base/
COPY . .
WORKDIR /src/src/ApiGateways/ApiGw-Base/
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]
