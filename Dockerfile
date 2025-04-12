# syntax=docker/dockerfile:1

# build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 as build
WORKDIR /src

# cache dependencies
COPY PickEm.Api.sln .
COPY *.csproj .
RUN dotnet restore PickEm.Api.sln

# build executable
COPY . .
RUN dotnet publish -c Release -o /publish

# prod stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 as production
WORKDIR /publish
COPY --from=build /publish .

# networking for incoming traffic
EXPOSE 8443

#ENV
ENV ASPNETCORE_URLS="http://+:8443"
ENV PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"
ENV DOTNET_RUNNING_IN_CONTAINER="true"
ENV DOTNET_VERSION="8.0.404"
ENV ASPNET_VERSION="8.0.404"
ENV BASE_PATH="/api"
ENV ENABLE_SWAGGER="false"
ENV MIGRATE_DATABASE="false"
ENV ConnectionStrings__DefaultConnection=""

# start
ENTRYPOINT ["dotnet", "PickEm.Api.dll"]