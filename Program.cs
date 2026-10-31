using KbApi.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//注册 ProblemDetails 服务。
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IKnowledgeBaseService, KnowledgeBaseService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//捕获所有没处理的异常，返回 500 的 ProblemDetails，而不是报错堆栈。
app.UseExceptionHandler();
//让没有响应体的错误（比如 404）也带上 ProblemDetails。
app.UseStatusCodePages();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
