# 使用 SDK 镜像构建
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# 复制 csproj 并还原依赖
COPY *.csproj ./
RUN dotnet restore

# 复制剩余文件并构建发布版本
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# 使用运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Render 会设置 PORT 环境变量
ENV ASPNETCORE_URLS=http://*:${PORT:-5000}

# 暴露端口
EXPOSE 5050

# 启动应用
ENTRYPOINT ["dotnet", "Korea.dll"]
