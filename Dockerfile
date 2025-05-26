# Use a imagem base otimizada do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Criar usuário não-root
RUN addgroup -g 1001 -S appgroup && \
    adduser -S appuser -u 1001 -G appgroup

# Use a imagem SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["*.csproj", "./"]
RUN dotnet restore

# Copiar código fonte e fazer build
COPY . .
RUN dotnet build -c Release -o /app/build

# Publicar aplicação
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app

# Copiar arquivos publicados
COPY --from=publish /app/publish .

# Mudar para usuário não-root
USER appuser

# Configurar variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "SuaAPI.dll"]
