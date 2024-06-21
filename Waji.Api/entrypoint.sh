#!/bin/sh

# Apply database migrations
dotnet tool install --global dotnet-ef
export PATH="$PATH:/root/.dotnet/tools"
dotnet ef database update --project ../Waje.Api.Data

# Start the application
exec dotnet Waji.Api.dll
