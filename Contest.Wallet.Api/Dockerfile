#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Contest.Wallet.Api/Consent.Api.csproj", "Contest.Wallet.Api/"]
COPY ["Contest.Wallet.Common/Consent.Common.csproj", "Contest.Wallet.Common/"]
RUN dotnet restore "Contest.Wallet.Api/Consent.Api.csproj"
COPY . .
WORKDIR "/src/Contest.Wallet.Api"
RUN dotnet build "Consent.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consent.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consent.Api.dll"]