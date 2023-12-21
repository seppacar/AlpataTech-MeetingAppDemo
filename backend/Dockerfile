#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AlpataTech.MeetingAppDemo.API/AlpataTech.MeetingAppDemo.API.csproj", "AlpataTech.MeetingAppDemo.API/"]
COPY ["AlpataTech.MeetingAppDemo.DAL/AlpataTech.MeetingAppDemo.DAL.csproj", "AlpataTech.MeetingAppDemo.DAL/"]
COPY ["AlpataTech.MeetingAppDemo.Entities/AlpataTech.MeetingAppDemo.Entities.csproj", "AlpataTech.MeetingAppDemo.Entities/"]
COPY ["AlpataTech.MeetingAppDemo.Services/AlpataTech.MeetingAppDemo.Services.csproj", "AlpataTech.MeetingAppDemo.Services/"]
RUN dotnet restore "./AlpataTech.MeetingAppDemo.API/./AlpataTech.MeetingAppDemo.API.csproj"
COPY . .
WORKDIR "/src/AlpataTech.MeetingAppDemo.API"
RUN dotnet build "./AlpataTech.MeetingAppDemo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AlpataTech.MeetingAppDemo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlpataTech.MeetingAppDemo.API.dll"]