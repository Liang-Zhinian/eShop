ARG NODE_IMAGE=node:8.11
FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.1 as dotnet-build
WORKDIR /src

FROM ${NODE_IMAGE} as node-build
WORKDIR /web
COPY src/Web/WebSPA .
RUN npm install
RUN npm run build:prod

FROM dotnet-build as publish
WORKDIR /src/src/Web/WebSPA/wwwroot
COPY --from=node-build /web/wwwroot .
WORKDIR /src
COPY . .
WORKDIR /src/src/Web/WebSPA
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebSPA.dll"]
