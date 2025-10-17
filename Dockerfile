# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY . .

# Restaurar dependencias
RUN dotnet restore ChallengeCompraGamer_Backend.sln

# Publicar el proyecto principal
RUN dotnet publish ./ChallengeCompraGamer_Backend.App/ChallengeCompraGamer_Backend.App.csproj -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ChallengeCompraGamer_Backend.App.dll"]
