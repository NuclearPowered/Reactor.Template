#!/bin/sh
dotnet pack templatepack.csproj -p:NoBuild=true -p:NoDefaultExcludes=true -c Release
