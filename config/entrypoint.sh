#!/bin/sh
# Optionally run a restore (if your local files changed dependencies, etc.)
dotnet restore

# Run dotnet watch for rapid iteration.
dotnet watch run --project src/CodeaculaStreamerTools.API/CodeaculaStreamerTools.API.csproj
