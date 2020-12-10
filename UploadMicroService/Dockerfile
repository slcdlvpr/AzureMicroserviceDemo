#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV dataconnectionstring=[Your Connection Info]
ENV storageconnectionstring=[[Your Connection Info]]
ENV containername=[Your container name]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UploadMicroService/UploadMicroService.csproj", "UploadMicroService/"]
COPY ["BlobStorage/BlobStorage.csproj", "BlobStorage/"]
COPY ["KeyVaultStore/KeyVaultStore.csproj", "KeyVaultStore/"]
COPY ["DataStore/DataStore.csproj", "DataStore/"]
RUN dotnet restore "UploadMicroService/UploadMicroService.csproj"
COPY . .
WORKDIR "/src/UploadMicroService"
RUN dotnet build "UploadMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UploadMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UploadMicroService.dll"]