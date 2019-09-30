FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ChauffeurApiCORE/ChauffeurApiCORE.csproj ChauffeurApiCORE/
RUN dotnet restore "ChauffeurApiCORE/ChauffeurApiCORE.csproj"
COPY . .
WORKDIR "/src/ChauffeurApiCORE"
RUN dotnet build "ChauffeurApiCORE.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChauffeurApiCORE.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChauffeurApiCORE.dll"]
