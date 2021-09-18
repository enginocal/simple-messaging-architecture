#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/User/Messaging.User.Services/Messaging.User.Services.csproj", "src/User/Messaging.User.Services/"]
COPY ["src/Shared/Messaging.User.Domain/Messaging.User.Domain.csproj", "src/Shared/Messaging.User.Domain/"]
COPY ["src/Shared/Messaging.Shared.Common/Messaging.Shared.Common.csproj", "src/Shared/Messaging.Shared.Common/"]
RUN dotnet restore "src/User/Messaging.User.Services/Messaging.User.Services.csproj"
COPY . .
WORKDIR "/src/src/User/Messaging.User.Services"
RUN dotnet build "Messaging.User.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messaging.User.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messaging.User.Services.dll"]