FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CsharpDapperExample.csproj", "CsharpDapperExample.csproj"]
RUN dotnet restore "CsharpDapperExample.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CsharpDapperExample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish CsharpDapperExample.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CsharpDapperExample.dll"]