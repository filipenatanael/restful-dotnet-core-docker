FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["RestfulWebServicesCalculator.csproj", "RestfulWebServicesCalculator/"]
RUN dotnet restore "RestfulWebServicesCalculator/RestfulWebServicesCalculator.csproj"
WORKDIR "/src/RestfulWebServicesCalculator"
COPY . .
RUN dotnet build "RestfulWebServicesCalculator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestfulWebServicesCalculator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestfulWebServicesCalculator.dll"]