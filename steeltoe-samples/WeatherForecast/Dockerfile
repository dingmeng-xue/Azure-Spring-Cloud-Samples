FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /WeatherForecast

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
WORKDIR /WeatherForecast
COPY --from=build /WeatherForecast/out .
ENTRYPOINT ["dotnet", "WeatherForecast.dll"]