# --------------------------
# Etapa 1: Build da aplicação
# --------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho
WORKDIR /src

# Copia os arquivos de solução e projeto
COPY *.sln .
COPY Mottu/*.csproj ./Mottu/

# Restaura os pacotes
RUN dotnet restore

# Copia o restante do código
COPY . .

# Define diretório de trabalho do projeto
WORKDIR /src/Mottu

# Publica a aplicação em Release
RUN dotnet publish -c Release -o /app

# --------------------------
# Etapa 2: Imagem final (runtime)
# --------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app .

# Define variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production

# Expõe a porta que a aplicação usará
EXPOSE 80

# Metadata
LABEL maintainer="Pedro Bergara <seu.email@dominio.com>"

# Define o comando de inicialização
ENTRYPOINT ["dotnet", "Mottu.dll"]
