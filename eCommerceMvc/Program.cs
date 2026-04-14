using eCommerceMvc.Context;
using eCommerceMvc.Services;
using eCommerceMvc.Services.Sync;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.Configure<TrendyolIntegrationOptions>(builder.Configuration.GetSection(TrendyolIntegrationOptions.SectionName));
builder.Services.Configure<ShopifyIntegrationOptions>(builder.Configuration.GetSection(ShopifyIntegrationOptions.SectionName));
builder.Services.AddScoped<IPageContentService, PageContentService>();
builder.Services.AddScoped<IPageMediaService, PageMediaService>();
builder.Services.AddScoped<PageRenderService>();
builder.Services.AddScoped<ITrendyolConnectionVerificationService, TrendyolConnectionVerificationService>();
builder.Services.AddScoped<IShopifyConnectionVerificationService, ShopifyConnectionVerificationService>();
builder.Services.AddScoped<ITrendyolProductSyncService, TrendyolProductSyncService>();
builder.Services.AddScoped<IShopifyProductSyncService, ShopifyProductSyncService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
