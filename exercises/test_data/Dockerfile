# Use .NET 8.0 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy solution and project files
COPY WordleStats.sln ./
COPY WordleStats/WordleStats.csproj ./WordleStats/
COPY WordleStats.Cli/WordleStats.Cli.csproj ./WordleStats.Cli/
COPY WordleStats.Tests/WordleStats.Tests.csproj ./WordleStats.Tests/

# Restore dependencies
RUN dotnet restore

# Copy data and CLI source code (not for editing)
COPY WordleStats.Cli/ ./WordleStats.Cli/
COPY data/ ./data/

# Note: WordleStats/ and WordleStats.Tests/ will be mounted as volumes
# This allows learners to edit code outside the container and see changes reflected

# Test stage - builds from volume-mounted source
FROM build AS tests
WORKDIR /app
CMD ["dotnet", "test", "-c", "Release", "--verbosity", "normal"]

# Production stage - run the CLI app
FROM build AS cli
WORKDIR /app
CMD ["dotnet", "run", "--project", "WordleStats.Cli", "-c", "Release"]
