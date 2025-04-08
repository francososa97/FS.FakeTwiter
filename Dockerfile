# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY FS.FakeTwitter.Api/*.csproj ./FS.FakeTwitter.Api/
COPY FS.FakeTwitter.Application/*.csproj ./FS.FakeTwitter.Application/
COPY FS.FakeTwitter.Domain/*.csproj ./FS.FakeTwitter.Domain/
COPY FS.FakeTwitter.Infrastructure/*.csproj ./FS.FakeTwitter.Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /app/FS.FakeTwitter.Api
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/FS.FakeTwitter.Api/out ./

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "FS.FakeTwitter.Api.dll"]
