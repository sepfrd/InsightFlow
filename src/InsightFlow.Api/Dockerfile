FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InsightFlow.Api/InsightFlow.Api.csproj", "src/InsightFlow.Api/"]
COPY ["src/InsightFlow.Domain/InsightFlow.Domain.csproj", "src/InsightFlow.Domain/"]
COPY ["src/InsightFlow.Application/InsightFlow.Application.csproj", "src/InsightFlow.Application/"]
COPY ["src/InsightFlow.Common/InsightFlow.Common.csproj", "src/InsightFlow.Common/"]
COPY ["src/InsightFlow.Infrastructure/InsightFlow.Infrastructure.csproj", "src/InsightFlow.Infrastructure/"]
RUN dotnet restore "src/InsightFlow.Api/InsightFlow.Api.csproj"
COPY . .
WORKDIR "/src/src/InsightFlow.Api"
RUN dotnet build "InsightFlow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InsightFlow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InsightFlow.Api.dll"]
