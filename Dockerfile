

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 8080

# # Copy the published Blazor app to the container
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# COPY . .
# WORKDIR "/src/AutoShopWeb"
# # RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
# # RUN chmod +x ./dotnet-install.sh
# # RUN ./dotnet-install.sh --version latest
# # RUN dotnet workload restore
# RUN dotnet publish AutoShopWeb.csproj-c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/AutoShopWeb"

# Replace `YourProject.csproj` with the actual name of your project file
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AutoShopWeb.dll"]


