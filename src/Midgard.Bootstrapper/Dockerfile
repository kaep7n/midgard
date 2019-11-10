FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Midgard.Bootstrapper/Midgard.Bootstrapper.csproj", "Midgard.Bootstrapper/"]
COPY ["Midgard/Midgard.csproj", "Midgard/"]
COPY ["Midgard.Hosting/Midgard.Hosting.csproj", "Midgard.Hosting/"]
RUN dotnet restore "Midgard.Bootstrapper/Midgard.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/Midgard.Bootstrapper"
RUN dotnet build "Midgard.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Midgard.Bootstrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Midgard.Bootstrapper.dll"]