
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BlakeBot.Web.Api/*.csproj ./BlakeBot.Web.Api/
COPY BlakeBot.Tests/*.csproj ./BlakeBot.Tests/
RUN dotnet restore

# copy everything else and build app
COPY BlakeBot.Web.Api/. ./BlakeBot.Web.Api/
WORKDIR /BlakeBot.Web.Api
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /
COPY --from=build /BlakeBot.Web.Api/out ./
ENTRYPOINT ["dotnet", "BlakeBot.Web.Api.dll"]