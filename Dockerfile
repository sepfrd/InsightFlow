# Build

FROM mcr.microsoft.com/dotnet/sdk AS build

WORKDIR /source

COPY . .

RUN dotnet restore ./

# Serve

