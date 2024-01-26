#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Training.Api/Training.API.csproj", "src/Training.Api/"]
COPY ["tonugets/Training.NG.HttpResponse/Training.NG.HttpResponse.csproj", "tonugets/Training.NG.HttpResponse/"]
COPY ["src/Training.Application/Training.Application.csproj", "src/Training.Application/"]
COPY ["src/Training.Infra/Training.Infra.csproj", "src/Training.Infra/"]
COPY ["src/Training.Core/Training.Core.csproj", "src/Training.Core/"]
COPY ["tonugets/Training.NG.EFCommon/Training.NG.EFCommon.csproj", "tonugets/Training.NG.EFCommon/"]
RUN dotnet restore "src/Training.Api/Training.API.csproj"
COPY . .
WORKDIR "/src/src/Training.Api"
RUN dotnet build "Training.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Training.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Training.API.dll"]