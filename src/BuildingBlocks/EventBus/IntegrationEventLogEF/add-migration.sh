#!/bin/sh

dotnet ef --startup-project ../../../Services/Catalog/Catalog.API migrations add Initial --context IntegrationEventLogContext
