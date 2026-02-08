//var builder = WebApplication.CreateBuilder(args);
//
//// 注册 MVC 和 HttpClient
//builder.Services.AddControllersWithViews();
//builder.Services.AddHttpClient();
//
//var app = builder.Build();
//
//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//app.Urls.Add($"http://*:{port}");
//
//// 静态文件
//app.UseStaticFiles();
//
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}
//
//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseAuthorization();
//
//// Map MVC 路由
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//
//// Map API Controller
//app.MapControllers();
//
//app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
// -------- 数据模拟 --------
var people = new List<Person> {
    new Person{ Id=1, Name="周", Target=10000 },
    new Person{ Id=2, Name="何", Target=7000 },
    new Person{ Id=3, Name="沈", Target=10000 },
    new Person{ Id=4, Name="李", Target=20000 },
    new Person{ Id=5, Name="泉", Target=10000 }
};

var transactions = new List<Transaction>();
var todos = new List<Todo>();
var nextTodoId = 1;

// -------- API --------

// 获取所有人
app.MapGet("/api/people", () => people);

// 获取某人的交易记录
app.MapGet("/api/people/{id}/transactions", (int id) =>
{
    return transactions.Where(t => t.PersonId == id).ToList();
});

// 添加交易
app.MapPost("/api/people/{id}/transactions", (int id, TransactionInput input) =>
{
    var t = new Transaction
    {
        PersonId = id,
        Type = input.Type,
        Amount = input.Amount,
        Date = DateTime.Now
    };
    transactions.Add(t);
    return Results.Ok(t);
});

// ToDo 获取
app.MapGet("/api/todos", () => todos);

// 添加 ToDo
app.MapPost("/api/todos", (TodoInput input) =>
{
    var t = new Todo
    {
        Id = nextTodoId++,
        Text = input.Text,
        Done = false
    };
    todos.Add(t);
    return Results.Ok(t);
});

// 切换完成
app.MapPut("/api/todos/{id}", (int id) =>
{
    var t = todos.FirstOrDefault(x => x.Id == id);
    if (t == null) return Results.NotFound();
    t.Done = !t.Done;
    return Results.Ok(t);
});

// 删除 ToDo
app.MapDelete("/api/todos/{id}", (int id) =>
{
    var t = todos.FirstOrDefault(x => x.Id == id);
    if (t != null) todos.Remove(t);
    return Results.Ok();
});

// 简单汇率 API（模拟）
app.MapGet("/api/exchange", (string from, string to) =>
{
    if (from=="KRW" && to=="CNY") return Results.Ok(new { rate = 0.005 });
    return Results.Ok(new { rate = 1.0 });
});

app.Run();

// -------- 数据类 --------
record Person { public int Id {get;set;} public required string Name {get;set;} public decimal Target {get;set;} }
record Transaction { public int PersonId {get;set;} public required string Type {get;set;} public decimal Amount {get;set;} public DateTime Date {get;set;} }
record TransactionInput { public required string Type {get;set;} public decimal Amount {get;set;} }
record Todo { public int Id {get;set;} public required string Text {get;set;} public bool Done {get;set;} }
record TodoInput { public required string Text {get;set;} }
