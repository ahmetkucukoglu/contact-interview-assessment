FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Consumers/ContactApp.OutboxProcessor/ContactApp.OutboxProcessor.csproj", "src/Consumers/ContactApp.OutboxProcessor/"]
RUN dotnet restore "src/Consumers/ContactApp.OutboxProcessor/ContactApp.OutboxProcessor.csproj"
COPY . .
WORKDIR "/src/src/Consumers/ContactApp.OutboxProcessor"
RUN dotnet build "ContactApp.OutboxProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactApp.OutboxProcessor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactApp.OutboxProcessor.dll"]
