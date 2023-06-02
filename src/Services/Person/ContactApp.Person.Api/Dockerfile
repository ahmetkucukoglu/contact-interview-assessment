FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Person/ContactApp.Person.Api/ContactApp.Person.Api.csproj", "src/Services/Person/ContactApp.Person.Api/"]
RUN dotnet restore "src/Services/Person/ContactApp.Person.Api/ContactApp.Person.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Person/ContactApp.Person.Api"
RUN dotnet build "ContactApp.Person.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactApp.Person.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactApp.Person.Api.dll"]
