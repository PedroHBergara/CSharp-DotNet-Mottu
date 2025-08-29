# Imagem base para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5297

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["challengeABD/challengeABD.csproj", "./challengeABD/"]
RUN dotnet restore "./challengeABD/challengeABD.csproj"


COPY . .
WORKDIR "/src/challengeABD"
RUN dotnet build "challengeABD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "challengeABD.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "challengeABD.dll"]
