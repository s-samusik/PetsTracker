FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PT.Blazor/PT.Blazor.csproj PT.Blazor/
COPY PT.Application/PT.Application.csproj PT.Application/
COPY PT.Infrastructure/PT.Infrastructure.csproj PT.Infrastructure/
COPY PT.Domain/PT.Domain.csproj PT.Domain/

RUN dotnet restore PT.Blazor/PT.Blazor.csproj

COPY . .

RUN dotnet publish PT.Blazor/PT.Blazor.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "PT.Blazor.dll"]