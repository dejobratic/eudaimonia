#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Eudaimonia.Api/Eudaimonia.Api.csproj", "src/Eudaimonia.Api/"]
RUN dotnet restore "src/Eudaimonia.Api/Eudaimonia.Api.csproj"
COPY . .
WORKDIR "/src/src/Eudaimonia.Api"
RUN dotnet build "Eudaimonia.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eudaimonia.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eudaimonia.Api.dll"]