FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ContactApp.Gateway/ContactApp.Gateway.csproj", "src/ContactApp.Gateway/"]
RUN dotnet restore "src/ContactApp.Gateway/ContactApp.Gateway.csproj"
COPY . .
WORKDIR "/src/src/ContactApp.Gateway"
RUN dotnet build "ContactApp.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactApp.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactApp.Gateway.dll"]
