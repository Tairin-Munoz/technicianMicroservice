FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["technicianMicroserviceAPI/technicianMicroservice.API.csproj", "technicianMicroserviceAPI/"]
COPY ["technicianMicroservice.Application/technicianMicroservice.Application.csproj", "technicianMicroservice.Application/"]
COPY ["technicianMicroservice.Domain/technicianMicroservice.Domain.csproj", "technicianMicroservice.Domain/"]
COPY ["technicianMicroservice.Infrastructure/technicianMicroservice.Infrastructure.csproj", "technicianMicroservice.Infrastructure/"]

RUN dotnet restore "technicianMicroserviceAPI/technicianMicroservice.API.csproj"

COPY . .

WORKDIR "/src/technicianMicroserviceAPI"
RUN dotnet build "technicianMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "technicianMicroservice.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "technicianMicroservice.API.dll"]
