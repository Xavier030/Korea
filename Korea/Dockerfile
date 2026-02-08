# 使用 SDK 构建阶段
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# 复制 csproj 并还原依赖
COPY ./Korea.csproj ./
RUN dotnet restore

# 复制剩余文件并构建发布版本
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# 使用 ASP.NET 运行时
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# 环境变量端口
ENV PORT=5000
EXPOSE $PORT
ENTRYPOINT ["dotnet", "Korea.dll"]
