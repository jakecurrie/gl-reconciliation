FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/GLRecon.Api/GLRecon.Api.csproj", "src/GLRecon.Api/"]
COPY ["src/GLRecon.Infrastructure/GLRecon.Infrastructure.csproj", "src/GLRecon.Infrastructure/"]
COPY ["src/GLRecon.Domain/GLRecon.Domain.csproj", "src/GLRecon.Domain/"]
RUN dotnet restore "src/GLRecon.Api/GLRecon.Api.csproj"
COPY . .
RUN dotnet publish "src/GLRecon.Api/GLRecon.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
USER app
ENTRYPOINT ["dotnet", "GLRecon.Api.dll"]
