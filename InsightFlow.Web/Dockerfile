FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InsightFlow.Web/InsightFlow.Web.csproj", "InsightFlow.Web/"]
COPY ["InsightFlow.Api/InsightFlow.Api.csproj", "InsightFlow.Api/"]
COPY ["InsightFlow.Common/InsightFlow.Common.csproj", "InsightFlow.Common/"]
COPY ["InsightFlow.Model/InsightFlow.Model.csproj", "InsightFlow.Model/"]
COPY ["InsightFlow.Business/InsightFlow.Business.csproj", "InsightFlow.Business/"]
COPY ["InsightFlow.DataAccess/InsightFlow.DataAccess.csproj", "InsightFlow.DataAccess/"]
RUN dotnet restore "InsightFlow.Web/InsightFlow.Web.csproj"
COPY . .
WORKDIR "/src/InsightFlow.Web"
RUN dotnet build "InsightFlow.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InsightFlow.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InsightFlow.Web.dll"]
