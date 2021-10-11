FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["csharp-dapper-example.csproj", "csharp-dapper-example.csproj"]
RUN dotnet restore "csharp-dapper-example.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "csharp-dapper-example.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish csharp-dapper-example.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "csharp-dapper-example.dll"]