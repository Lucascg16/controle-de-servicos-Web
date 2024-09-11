FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=https://+;http://+
ENV ASPNETCORE_HTTPS_PORTS=8081
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
#ENV	ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServicoInWeb.csproj", "ServicoInWeb/"]
RUN dotnet restore "./ServicoInWeb/ServicoInWeb.csproj"
WORKDIR "/src/ServicoInWeb"
COPY . .
RUN dotnet build "./ServicoInWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServicoInWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicoInWeb.dll"]