using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DataLayer.Data.Entities;
using BusinessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<BarDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BarDbContext>();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout = new LockoutOptions { AllowedForNewUsers = true, DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10), MaxFailedAccessAttempts = 5 };
})
               .AddEntityFrameworkStores<BarDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();

builder.Services.AddScoped<BarServices>();
builder.Services.AddScoped<ReviewServices>();
builder.Services.AddScoped<UserServices>();

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();

});

app.Run();



