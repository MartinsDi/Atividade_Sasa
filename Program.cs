var builder = WebApplication.CreateBuilder(args);

// Add services to the container..


/* adicionar o addDistributed e o addSession para funcionar a session*/
/* 
Abaixo são exemplos de configuração para adicionar a sessao
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
*/

builder.Services.AddSession();//Para funcionar a session
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();// Para funcionar a session

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
