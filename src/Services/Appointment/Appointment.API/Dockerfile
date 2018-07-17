FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY CqrsFramework.sln ./
COPY Sample/Reservation/v1/Registration/Registration.ClientWebApi/Registration.ClientWebApi.csproj Sample/Reservation/v1/Registration/Registration.ClientWebApi/
COPY Framework/CqrsFramework.Bus.RabbitMQ/CqrsFramework.Bus.RabbitMQ.csproj Framework/CqrsFramework.Bus.RabbitMQ/
COPY Framework/CqrsFramework/CqrsFramework.csproj Framework/CqrsFramework/
COPY Sample/Reservation/v1/Registration/Registration.Application/Registration.Application.csproj Sample/Reservation/v1/Registration/Registration.Application/
COPY Sample/Reservation/v1/Registration/Registration.Infra.Data/Registration.Infra.Data.csproj Sample/Reservation/v1/Registration/Registration.Infra.Data/
COPY Sample/Reservation/v1/Registration/Registration.Domain/Registration.Domain.csproj Sample/Reservation/v1/Registration/Registration.Domain/
COPY Sample/Reservation/v1/Registration/Registration.Contracts/Registration.Contracts.csproj Sample/Reservation/v1/Registration/Registration.Contracts/
COPY Sample/Reservation/v1/Business/Business.Contracts/Business.Contracts.csproj Sample/Reservation/v1/Business/Business.Contracts/
COPY Sample/SaaSEqt/Common/SaaSEqt.Common.csproj Sample/SaaSEqt/Common/
COPY Framework/CqrsFramework.Extensions/CqrsFramework.Extensions.csproj Framework/CqrsFramework.Extensions/
COPY Sample/SaaSEqt/IdentityAccess/IdentityAccess/SaaSEqt.IdentityAccess.csproj Sample/SaaSEqt/IdentityAccess/IdentityAccess/
COPY Sample/SaaSEqt/IdentityAccess/Application/SaaSEqt.Application.csproj Sample/SaaSEqt/IdentityAccess/Application/
COPY Sample/SaaSEqt/IdentityAccess/IdentityAccess.Infra.Data/SaaSEqt.IdentityAccess.Infra.Data.csproj Sample/SaaSEqt/IdentityAccess/IdentityAccess.Infra.Data/
COPY Sample/Reservation/v1/Infrastructure.IoC/Infrastructure.IoC.csproj Sample/Reservation/v1/Infrastructure.IoC/
COPY Sample/Infrastructure/HealthChecks/src/Microsoft.AspNetCore.HealthChecks/Microsoft.AspNetCore.HealthChecks.csproj Sample/Infrastructure/HealthChecks/src/Microsoft.AspNetCore.HealthChecks/
COPY Sample/Infrastructure/HealthChecks/src/Microsoft.Extensions.HealthChecks/Microsoft.Extensions.HealthChecks.csproj Sample/Infrastructure/HealthChecks/src/Microsoft.Extensions.HealthChecks/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Sample/Reservation/v1/Registration/Registration.ClientWebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Registration.ClientWebApi.dll"]
