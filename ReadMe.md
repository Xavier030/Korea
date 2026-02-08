▶️ 如何运行

cd Korea
dotnet run

▶️ 如何推送docker

进入 项目根目录（上层 Korea/）：

cd "/Users/sherry/Documents/Git Korea/Korea"


用 -f 指定 Dockerfile 的路径：

docker build -t korea-webservice -f Korea/Dockerfile .

------Dockerfile：
# 使用 SDK 镜像构建
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# 复制 csproj 并还原依赖
COPY Korea/Korea.csproj ./Korea.csproj
RUN dotnet restore

# 复制整个项目
COPY . ./

# 发布
RUN dotnet publish -c Release -o /app/publish

# 运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENV PORT 5000
EXPOSE $PORT

ENTRYPOINT ["dotnet", "Korea.dll"]
