FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5042

ENV ASPNETCORE_URLS=http://+:5042

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["app-dotnet.csproj", "./"]
RUN dotnet restore "app-dotnet.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "app-dotnet.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "app-dotnet.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app-dotnet.dll"]
