#!/bin/bash

echo "Restoring dependencies..."
dotnet restore

echo "Building project..."
dotnet build --configuration Release

echo "Publishing..."
dotnet publish --configuration Release --output ./out

echo "✅ Done."
