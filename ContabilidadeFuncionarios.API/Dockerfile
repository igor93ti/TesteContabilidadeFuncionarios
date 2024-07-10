FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ContabilidadeFuncionarios.API/ContabilidadeFuncionarios.API.csproj", "ContabilidadeFuncionarios.API/"]
COPY ["ContabilidadeFuncionarios.Application/ContabilidadeFuncionarios.Application.csproj", "ContabilidadeFuncionarios.Application/"]
COPY ["ContabilidadeFuncionarios.Infrastructure/ContabilidadeFuncionarios.Infrastructure.csproj", "ContabilidadeFuncionarios.Infrastructure/"]
COPY ["ContabilidadeFuncionarios.Domain/ContabilidadeFuncionarios.Domain.csproj", "ContabilidadeFuncionarios.Domain/"]
RUN dotnet restore "ContabilidadeFuncionarios.API/ContabilidadeFuncionarios.API.csproj"
COPY . .
WORKDIR "/src/ContabilidadeFuncionarios.API"
RUN dotnet build "ContabilidadeFuncionarios.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContabilidadeFuncionarios.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContabilidadeFuncionarios.API.dll"]
