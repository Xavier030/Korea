//var builder = WebApplication.CreateBuilder(args);
//
//// 先注册服务
//builder.Services.AddControllersWithViews();
//builder.Services.AddHttpClient();
//
//var app = builder.Build();
//
//app.UseStaticFiles();
//
//// Configure the HTTP request pipeline.
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
//app.MapStaticAssets();
//
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();
//
//app.Run();

var builder = WebApplication.CreateBuilder(args);

// 注册 MVC 和 HttpClient
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// 静态文件
app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map MVC 路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API Controller
app.MapControllers();

app.Run();
