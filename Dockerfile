# base image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Agricultural/Agricultural.csproj", "Agricultural/"]
COPY ["Agricultural.Core/Agricultural.Core.csproj", "Agricultural.Core/"]
COPY ["Agricultural.Repo/Agricultural.Repo.csproj", "Agricultural.Repo/"]
COPY ["Agricultural.Serv/Agricultural.Serv.csproj", "Agricultural.Serv/"]

RUN dotnet restore "Agricultural/Agricultural.csproj"

COPY . .

WORKDIR /src/Agricultural
RUN dotnet build "Agricultural.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .

COPY Agricultural.Repo/Data /app/Data

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Agricultural.dll"]
