#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjetoCompeticao.API/ProjetoCompeticao.Worker.API", "ProjetoCompeticao.API/"]
COPY ["ProjetoCompeticao.Extensions/ProjetoCompeticao.Extensions.csproj", "ProjetoCompeticao.Extensions/"]
COPY ["ProjetoCompeticao.Shared/ProjetoCompeticao.Shared.csproj", "ProjetoCompeticao.Shared/"]
COPY ["ProjetoCompeticao.Tests/ProjetoCompeticao.Tests.csproj", "ProjetoCompeticao.Tests/"]
COPY ["ProjetoCompeticao.Domain/ProjetoCompeticao.Domain.csproj", "ProjetoCompeticao.Domain/"]
COPY ["ProjetoCompeticao.Application/ProjetoCompeticao.Application.csproj", "ProjetoCompeticao.Application/"]
COPY ["ProjetoCompeticao.Infra.Data/ProjetoCompeticao.Infra.Data.csproj", "ProjetoCompeticao.Infra.Data/"]
RUN dotnet restore "ProjetoCompeticao.API/ProjetoCompeticao.API.csproj"
COPY . .
WORKDIR "/src/ProjetoCompeticao.API"
RUN dotnet build "ProjetoCompeticao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjetoCompeticao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoCompeticao.API.dll"]

