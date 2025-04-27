# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY [Agricultural/Agricultural.csproj, Agricultural/]
COPY [Agricultural.Core/Agricultural.Core.csproj, Agricultural.Core/]
COPY [Agricultural.Repo/Agricultural.Repo.csproj, Agricultural.Repo/]
COPY [Agricultural.Serv/Agricultural.Serv.csproj, Agricultural.Serv/]

RUN dotnet restore "Agricultural/Agricultural.csproj"

COPY . .   # هنا يتم نسخ كل شيء، تأكد أن مجلد Data موجود بجانب باقي المشاريع
WORKDIR /src/Agricultural
RUN dotnet build "Agricultural.csproj" -c Release -o /app/build

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/build .   # نسخ الـ output
COPY --from=build /src/Agricultural.Repo/Data /app/Data   # نسخ مجلد البيانات
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Agricultural.dll"]
