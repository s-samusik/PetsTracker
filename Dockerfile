# ============================
# BUILD STAGE
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Кэшируем NuGet пакеты между билдами
# Требует BuildKit (он включён по умолчанию в GitHub Actions)
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    echo "NuGet cache mounted"

# Копируем только csproj — это позволяет кэшировать restore
COPY PT.Blazor/PT.Blazor.csproj PT.Blazor/
COPY PT.Application/PT.Application.csproj PT.Application/
COPY PT.Infrastructure/PT.Infrastructure.csproj PT.Infrastructure/
COPY PT.Domain/PT.Domain.csproj PT.Domain/

# Выполняем restore с кэшем
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore PT.Blazor/PT.Blazor.csproj

# Теперь копируем весь проект
COPY . .

# Публикуем
RUN dotnet publish PT.Blazor/PT.Blazor.csproj -c Release -o /app/publish /p:UseAppHost=false


# ============================
# RUNTIME STAGE
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "PT.Blazor.dll"]
