FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RedditMockup.Web/RedditMockup.Web.csproj", "RedditMockup.Web/"]
COPY ["RedditMockup.Api/RedditMockup.Api.csproj", "RedditMockup.Api/"]
COPY ["RedditMockup.Common/RedditMockup.Common.csproj", "RedditMockup.Common/"]
COPY ["RedditMockup.Model/RedditMockup.Model.csproj", "RedditMockup.Model/"]
COPY ["RedditMockup.Business/RedditMockup.Business.csproj", "RedditMockup.Business/"]
COPY ["RedditMockup.DataAccess/RedditMockup.DataAccess.csproj", "RedditMockup.DataAccess/"]
RUN dotnet restore "RedditMockup.Web/RedditMockup.Web.csproj"
COPY . .
WORKDIR "/src/RedditMockup.Web"
RUN dotnet build "RedditMockup.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RedditMockup.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedditMockup.Web.dll"]
