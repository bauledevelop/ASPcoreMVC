using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Business.Mapping;
using Shop.Entities.Enities;
using Shop.Mvc.Entensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Shop.Common.MailHelper;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
var emailConfig = builder.Configuration.GetSection("EmailConfiguration");
builder.Services.Configure<MailSettings>(emailConfig);
builder.Services.AddSingleton(emailConfig);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
//.AddGoogle( options =>
//{
//    //var ggconfig = builder.Configuration.GetSection("Authentication:Google");
//    options.ClientId = "644252072398-ctfv5037thbij05loq4j99fl4a5m4672.apps.googleusercontent.com";
//    options.ClientSecret = "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";
//});
builder.Services.AddControllers();
builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDb")).UseLazyLoadingProxies());
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
});
IMapper _mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(_mapper);
builder.Services.RegisterDependencies();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Areas/Admin")),
        RequestPath = "/Areas/Admin"
});
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Commons")),
    RequestPath = "/Commons"
});
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "assets")),
    RequestPath = "/assets"
});
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});
app.Run();
