FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Fs.Api/Fs.Api.csproj", "src/Fs.Api/"]
COPY ["src/Fs.Infrastructure.WriteStorage/Fs.Infrastructure.Storage.csproj", "src/Fs.Infrastructure.WriteStorage/"]
COPY ["src/Fs.Domain/Fs.Domain.csproj", "src/Fs.Domain/"]
COPY ["src/Fs.Infrastructure/Fs.Infrastructure.csproj", "src/Fs.Infrastructure/"]
COPY ["src/Fs.Application/Fs.Application.csproj", "src/Fs.Application/"]
COPY ["src/Fs.Infrastructure.ReadStorage/Fs.Infrastructure.ReadStorage.csproj", "src/Fs.Infrastructure.ReadStorage/"]
COPY ["src/Fs.BackgroundTasks/Fs.BackgroundTasks.csproj", "src/Fs.BackgroundTasks/"]
RUN dotnet restore "src/Fs.Api/Fs.Api.csproj"
COPY . .
WORKDIR "/src/src/Fs.Api"
RUN dotnet build "Fs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fs.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fs.Api.dll"]
