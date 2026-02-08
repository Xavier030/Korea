# 使用官方 .NET SDK 镜像编译项目
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# 复制 csproj 并还原依赖（加速 build）
COPY *.csproj ./
RUN dotnet restore

# 复制整个项目
COPY . ./

# 发布到 /app/out
RUN dotnet publish -c Release -o out

# 使用运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app/out ./

# Render 会给我们分配端口
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

# 启动项目
ENTRYPOINT ["dotnet", "Korea.dll"]
