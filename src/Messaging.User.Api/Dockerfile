#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Messaging.User.Api/Messaging.User.Api.csproj", "src/Messaging.User.Api/"]
COPY ["src/Messaging.Shared.Common/Messaging.Shared.Common.csproj", "src/Messaging.Shared.Common/"]
RUN dotnet restore "src/Messaging.User.Api/Messaging.User.Api.csproj"
COPY . .
WORKDIR "/src/src/Messaging.User.Api"
RUN dotnet build "Messaging.User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messaging.User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messaging.User.Api.dll"]
