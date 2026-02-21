FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY AuthorizationService.slnx .

COPY AuthorizationService.Domain/AuthorizationService.Domain.csproj AuthorizationService.Domain/
COPY AuthorizationService.Persistence/AuthorizationService.Persistence.csproj AuthorizationService.Persistence/
COPY AuthorizationService.Infrastructure/AuthorizationService.Infrastructure.csproj AuthorizationService.Infrastructure/
COPY AuthorizationService.Application/AuthorizationService.Application.csproj AuthorizationService.Application/
COPY AuthorizationService.Web/AuthorizationService.Web.csproj AuthorizationService.Web/

COPY AuthorizationService.Domain.Tests/AuthorizationService.Domain.Tests.csproj AuthorizationService.Domain.Tests/
COPY AuthorizationService.Persistence.Tests/AuthorizationService.Persistence.Tests.csproj AuthorizationService.Persistence.Tests/

RUN dotnet restore AuthorizationService.slnx

COPY . .

WORKDIR /src/AuthorizationService.Web

RUN dotnet publish AuthorizationService.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish/. .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "AuthorizationService.Web.dll"]
