#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the aspnet runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the sdk image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore dependencies
COPY ["AlpataTech.MeetingAppDemo.API/AlpataTech.MeetingAppDemo.API.csproj", "AlpataTech.MeetingAppDemo.API/"]
COPY ["AlpataTech.MeetingAppDemo.DAL/AlpataTech.MeetingAppDemo.DAL.csproj", "AlpataTech.MeetingAppDemo.DAL/"]
COPY ["AlpataTech.MeetingAppDemo.Entities/AlpataTech.MeetingAppDemo.Entities.csproj", "AlpataTech.MeetingAppDemo.Entities/"]
COPY ["AlpataTech.MeetingAppDemo.Services/AlpataTech.MeetingAppDemo.Services.csproj", "AlpataTech.MeetingAppDemo.Services/"]
RUN dotnet restore "./AlpataTech.MeetingAppDemo.API"

# Copy the rest of the application
COPY . .

# Build the application
WORKDIR "/src"
RUN dotnet build "./AlpataTech.MeetingAppDemo.API" -c $BUILD_CONFIGURATION -o /app/build

# Install the dotnet ef tool
RUN dotnet tool install --global dotnet-ef --version 7.0.0
ENV PATH="$PATH:/root/.dotnet/tools"

# Generate self-contained EF Core migrations bundle
RUN dotnet ef migrations bundle --self-contained -r linux-x64 -o migrations_bundle --startup-project "./AlpataTech.MeetingAppDemo.API" --project "./AlpataTech.MeetingAppDemo.DAL"

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AlpataTech.MeetingAppDemo.API" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["AlpataTech.MeetingAppDemo.API/Resources", "./Resources"]
COPY --from=build /src/migrations_bundle /app/migrations_bundle
COPY --from=publish /app/publish .

# Set the entry point to start the application
ENTRYPOINT ["dotnet", "AlpataTech.MeetingAppDemo.API.dll"]