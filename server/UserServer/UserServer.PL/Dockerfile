# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../UserServer.PL ./
COPY ../UserServer.BLL ../UserServer.BLL
COPY ../UserServer.DAL ../UserServer.DAL
RUN dotnet restore "UserServer.PL.csproj"
RUN dotnet publish "UserServer.PL.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "UserServer.PL.dll"]
