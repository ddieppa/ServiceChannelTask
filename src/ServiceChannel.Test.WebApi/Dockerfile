FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServiceChannel.Test.WebApi/ServiceChannel.Test.WebApi.csproj", "ServiceChannel.Test.WebApi/"]
RUN dotnet restore "ServiceChannel.Test.WebApi/ServiceChannel.Test.WebApi.csproj"
COPY . .
WORKDIR "/src/ServiceChannel.Test.WebApi"
RUN dotnet build "ServiceChannel.Test.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceChannel.Test.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceChannel.Test.WebApi.dll"]
