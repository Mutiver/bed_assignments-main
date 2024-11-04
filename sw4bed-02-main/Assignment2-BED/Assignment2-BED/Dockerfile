#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Assignment2-BED/Assignment2-BED.csproj", "Assignment2-BED/"]
RUN dotnet restore "Assignment2-BED/Assignment2-BED.csproj"
COPY . .
WORKDIR "/src/Assignment2-BED"
RUN dotnet build "Assignment2-BED.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assignment2-BED.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assignment2-BED.dll"]