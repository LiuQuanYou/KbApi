# KbApi

一个用 ASP.NET Core Web API（.NET 8）+ EF Core + PostgreSQL 实现的知识库 API，支持知识库和文档的增删改查。

## 环境要求

- .NET 8 SDK
- Docker（用来运行 PostgreSQL）
- dotnet-ef 工具 8.0.x

## 快速开始

### 1. 启动数据库

第一次运行，创建容器：

```bash
docker run -d --name kbapi-pg \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=kbapi \
  -p 5432:5432 \
  postgres:16
```

以后每次开机，先打开 Docker Desktop，再执行：

```bash
docker start kbapi-pg
```

连接字符串在 `appsettings.json` 的 `ConnectionStrings:Default` 里。

### 2. 建表

```bash
dotnet tool install --global dotnet-ef --version 8.0.11   # 只需安装一次
export PATH="$PATH:$HOME/.dotnet/tools"                    # 找不到 dotnet-ef 时加上
dotnet ef database update
```

修改了实体或 `AppDbContext` 之后，生成新的迁移再更新数据库：

```bash
dotnet ef migrations add <迁移名称>
dotnet ef database update
```

### 3. 运行

```bash
dotnet run
```

启动后打开 Swagger：http://localhost:5299/swagger

## 接口列表

### 知识库

| 方法 | 路由 | 说明 |
|---|---|---|
| GET | `/api/knowledge-bases` | 获取知识库列表 |
| GET | `/api/knowledge-bases/{id}` | 获取单个知识库 |
| POST | `/api/knowledge-bases` | 创建知识库 |
| PUT | `/api/knowledge-bases/{id}` | 修改知识库 |
| DELETE | `/api/knowledge-bases/{id}` | 删除知识库（会级联删除其下所有文档） |

### 文档

| 方法 | 路由 | 说明 |
|---|---|---|
| GET | `/api/knowledge-bases/{kbId}/documents` | 获取某知识库下的文档列表 |
| GET | `/api/knowledge-bases/{kbId}/documents/{docId}` | 获取单篇文档 |
| POST | `/api/knowledge-bases/{kbId}/documents` | 在知识库下创建文档 |
| DELETE | `/api/knowledge-bases/{kbId}/documents/{docId}` | 删除文档 |

错误统一以 ProblemDetails 格式返回（参数校验失败 400，资源不存在 404）。

## 项目结构

```
Controllers/   控制器，处理 HTTP 请求
Services/      业务逻辑，通过 AppDbContext 访问数据库
Data/          AppDbContext（EF Core 数据库上下文与模型配置）
Models/        实体类
Dtos/          请求与响应的数据结构
Migrations/    EF Core 迁移文件
```
