#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["seamless.bigwin.one.csproj", "."]
RUN dotnet restore "./seamless.bigwin.one.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "seamless.bigwin.one.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "seamless.bigwin.one.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "seamless.bigwin.one.dll"]