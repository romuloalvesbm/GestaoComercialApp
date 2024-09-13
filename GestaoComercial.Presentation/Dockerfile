# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GestaoComercial.Presentation/GestaoComercial.Presentation.csproj", "GestaoComercial.Presentation/"]
COPY ["GestaoComercial.Application/GestaoComercial.Application.csproj", "GestaoComercial.Application/"]
COPY ["GestaoComercial.CrossCutting.Authorization/GestaoComercial.CrossCutting.Authorization.csproj", "GestaoComercial.CrossCutting.Authorization/"]
COPY ["GestaoComercial.Domain/GestaoComercial.Domain.csproj", "GestaoComercial.Domain/"]
COPY ["GestaoComercial.Infra.Data/GestaoComercial.Infra.Data.csproj", "GestaoComercial.Infra.Data/"]
COPY ["GestaoComercial.Infra.External.Identity/GestaoComercial.Infra.External.Identity.csproj", "GestaoComercial.Infra.External.Identity/"]
COPY ["GestaoComercial.Infra.Redis/GestaoComercial.Infra.Redis.csproj", "GestaoComercial.Infra.Redis/"]
RUN dotnet restore "./GestaoComercial.Presentation/GestaoComercial.Presentation.csproj"
COPY . .
WORKDIR "/src/GestaoComercial.Presentation"
RUN dotnet build "./GestaoComercial.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GestaoComercial.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Homolog
ENTRYPOINT ["dotnet", "GestaoComercial.Presentation.dll"]