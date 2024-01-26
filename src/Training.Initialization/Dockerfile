#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /TrainingMigration

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Training.Initialization/Training.Initialization.csproj", "src/Training.Initialization/"]
COPY ["src/Training.Core/Training.Core.csproj", "src/Training.Core/"]
COPY ["tonugets/Training.NG.EFCommon/Training.NG.EFCommon.csproj", "tonugets/Training.NG.EFCommon/"]
COPY ["src/Training.Infra/Training.Infra.csproj", "src/Training.Infra/"]
COPY ["src/Training.SqlServerMigration/Training.SqlServerMigration.csproj", "src/Training.SqlServerMigration/"]
RUN dotnet restore "src/Training.Initialization/Training.Initialization.csproj"
COPY . .
WORKDIR "/src/src/Training.Initialization"
RUN dotnet build "Training.Initialization.csproj" -c Release -o /TrainingMigration/build

FROM build AS publish
RUN dotnet publish "Training.Initialization.csproj" -c Release -o /TrainingMigration/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /TrainingMigration
COPY --from=publish /TrainingMigration/publish .
ENTRYPOINT ["dotnet", "Training.Initialization.dll"]