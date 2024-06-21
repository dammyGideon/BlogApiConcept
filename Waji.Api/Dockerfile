# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

############################ Migrations
# Stage for initializing the database
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS init-db
RUN dotnet tool install --global dotnet-ef
RUN apt-get update && apt-get install -y postgresql-client
ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /app
COPY . .

# Command to wait for the database to be ready and run migrations
ENTRYPOINT ["sh", "-c", "while ! pg_isready -h app_db -p 5432 -U postgres; do sleep 1; done && dotnet ef database update --project Waje.Api.Data --startup-project Waji.Api"]

# Stage for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files
COPY ["Waji.Api/Waji.Api.csproj", "Waji.Api/"]
COPY ["Waje.Api.Data/Waje.Api.Data.csproj", "Waje.Api.Data/"]
COPY ["Waji.Api.Shared/Waji.Api.Shared.csproj", "Waji.Api.Shared/"]
COPY ["Waji.Api.CQRS/Waji.Api.CQRS.csproj", "Waji.Api.CQRS/"]
COPY ["Waji.Api.Test/Waji.Api.Test.csproj", "Waji.Api.Test/"]

# Restore the dependencies
RUN dotnet restore "./Waji.Api/Waji.Api.csproj"

# Copy the remaining files
COPY . .

# Set the working directory and build the project
WORKDIR "/src/Waji.Api"
RUN dotnet build "./Waji.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


# Stage for running tests
FROM build AS test
WORKDIR "/src/Waji.Api.Test"
RUN dotnet test --logger "trx;LogFileName=test_results.trx" --results-directory /testresults



# Stage for publishing the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Waji.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Waji.Api.dll"]
