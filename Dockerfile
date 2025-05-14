FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
WORKDIR /src
COPY ["TasksTracker.TasksManager.Backend.Api/TasksTracker.TasksManager.Backend.Api.csproj", "TasksTracker.TasksManager.Backend.Api/"]
RUN dotnet restore "TasksTracker.TasksManager.Backend.Api/TasksTracker.TasksManager.Backend.Api.csproj"
COPY . .
WORKDIR "/src/TasksTracker.TasksManager.Backend.Api"
RUN dotnet build "TasksTracker.TasksManager.Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TasksTracker.TasksManager.Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TasksTracker.TasksManager.Backend.Api.dll"]