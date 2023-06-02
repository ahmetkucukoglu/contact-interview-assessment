FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Consumers/ContactApp.Consumers/ContactApp.Consumers.csproj", "src/Consumers/ContactApp.Consumers/"]
RUN dotnet restore "src/Consumers/ContactApp.Consumers/ContactApp.Consumers.csproj"
COPY . .
WORKDIR "/src/src/Consumers/ContactApp.Consumers"
RUN dotnet build "ContactApp.Consumers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactApp.Consumers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactApp.Consumers.dll"]
