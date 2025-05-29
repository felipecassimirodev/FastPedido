# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FastPedidoAPI/FastPedidoAPI.csproj", "FastPedidoAPI/"]
COPY ["FastPedidoApi.Application/FastPedidoApi.Application.csproj", "FastPedidoApi.Application/"]
COPY ["FastPedidoApi.Domain/FastPedidoApi.Domain.csproj", "FastPedidoApi.Domain/"]
COPY ["FastPedidoApi.Persitence/FastPedidoApi.Persitence.csproj", "FastPedidoApi.Persitence/"]
COPY ["PedidoConsumer/PedidoConsumer.csproj", "PedidoConsumer/"]
RUN dotnet restore "./FastPedidoAPI/FastPedidoAPI.csproj"
COPY . .
WORKDIR "/src/FastPedidoAPI"
RUN dotnet build "./FastPedidoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastPedidoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastPedidoAPI.dll"]