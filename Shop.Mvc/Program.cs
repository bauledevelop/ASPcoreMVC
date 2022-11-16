using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Business.Mapping;
using Shop.Entities.Enities;
using Shop.Mvc.Entensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

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
