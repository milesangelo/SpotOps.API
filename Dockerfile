# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY ./SpotOps.Api.csproj ./
RUN dotnet restore "./SpotOps.Api.csproj"

# Copy everything else and build
COPY . .

RUN rm -rf app.db
RUN dotnet tool install --global dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet ef database update
# Build the database
#RUN dotnet ef database update
# Build the project
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/app.db .
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SpotOps.Api.dll"]
