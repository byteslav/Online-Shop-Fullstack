FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 3000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["csharp-dapper-example/csharp-dapper-example.csproj", "csharp-dapper-example/"]
RUN dotnet restore "csharp-dapper-example/csharp-dapper-example.csproj"
COPY . .
WORKDIR "/src/csharp-dapper-example"
RUN dotnet build "csharp-dapper-example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "csharp-dapper-example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "csharp-dapper-example.dll"]
