#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Auth/Messaging.Idendity.Api/Messaging.Idendity.Api.csproj", "src/Auth/Messaging.Idendity.Api/"]
COPY ["src/Shared/Messaging.User.Domain/Messaging.User.Domain.csproj", "src/Shared/Messaging.User.Domain/"]
COPY ["src/Shared/Messaging.Shared.Common/Messaging.Shared.Common.csproj", "src/Shared/Messaging.Shared.Common/"]
RUN dotnet restore "src/Auth/Messaging.Idendity.Api/Messaging.Idendity.Api.csproj"
COPY . .
WORKDIR "/src/src/Auth/Messaging.Idendity.Api"
RUN dotnet build "Messaging.Idendity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messaging.Idendity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messaging.Idendity.Api.dll"]