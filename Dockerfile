FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LAB14/LAB14.csproj", "LAB14/"]
RUN dotnet restore "LAB14/LAB14.csproj"
COPY . .
WORKDIR "/src/LAB14"
RUN dotnet build "LAB14.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LAB14.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "LAB14.dll"]