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
# AJUSTADO: Copia o .csproj da pasta challengeABD
COPY ["challengeABD/*.csproj", "challengeABD/"]
RUN dotnet restore "challengeABD/challengeABD.csproj"

# Copiar todo o código fonte (incluindo a pasta challengeABD)
COPY . .

# AJUSTADO: Define o diretório de trabalho para a pasta do projeto antes do build
WORKDIR "/src/challengeABD"
RUN dotnet build "challengeABD.csproj" -c Release -o /app/build

# Publicar aplicação
# AJUSTADO: Publica a partir do arquivo de projeto correto
FROM build AS publish
RUN dotnet publish "challengeABD.csproj" -c Release -o /app/publish /p:UseAppHost=false

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

# AJUSTADO: Certifique-se que o nome da DLL está correto
ENTRYPOINT ["dotnet", "challengeABD.dll"] 
