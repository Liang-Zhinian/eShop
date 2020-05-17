#!/bin/sh

dotnet ef --startup-project ../Ordering.API migrations add Initial --context OrderingContext