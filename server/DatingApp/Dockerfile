# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ["DatingApp.Api/DatingApp.Api.csproj", "DatingApp.Api/"]
COPY ["DatingApp.Application/DatingApp.Application.csproj", "DatingApp.Application/"]
COPY ["DatingApp.Domain/DatingApp.Domain.csproj", "DatingApp.Domain/"]
COPY ["DatingApp.Infrastructure/DatingApp.Infrastructure.csproj", "DatingApp.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "DatingApp.Api/DatingApp.Api.csproj"

# Copy everything else
COPY . .

# Build
WORKDIR "/src/DatingApp.Api"
RUN dotnet build "DatingApp.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "DatingApp.Api.csproj" -c Release -o /app/publish

# Stage 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Устанавливаем переменные для работы с HTTPS
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=your_cert_password
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Устанавливаем зависимости для HTTPS
RUN apt-get update && apt-get install -y openssl && \
    mkdir -p /https

# Создаем самоподписанный сертификат (для разработки)
RUN openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout /https/aspnetapp.key \
    -out /https/aspnetapp.crt -subj "/CN=datingapp" \
    -days 3650 && \
    openssl pkcs12 -export -out /https/aspnetapp.pfx -inkey /https/aspnetapp.key \
    -in /https/aspnetapp.crt -passout pass:your_cert_password

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "DatingApp.Api.dll"]