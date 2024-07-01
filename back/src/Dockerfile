FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BooksManagement/BooksManagement.Api.csproj", "BooksManagement/"]
COPY ["BooksManagement.Application/BooksManagement.Application.csproj", "BooksManagement.Application/"]
COPY ["BooksManagement.Abstractions/BooksManagement.Infrastructure.Abstractions.csproj", "BooksManagement.Abstractions/"]
COPY ["BooksManagement.Core/BooksManagement.Core.csproj", "BooksManagement.Core/"]
COPY ["BooksManagement.Infrastructure/BooksManagement.Infrastructure.csproj", "BooksManagement.Infrastructure/"]
RUN dotnet restore "BooksManagement/BooksManagement.Api.csproj"
COPY . .
WORKDIR "/src/BooksManagement"
RUN dotnet build "./BooksManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "./BooksManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish
ENTRYPOINT ["dotnet", "/app/publish/BooksManagement.Api.dll"]