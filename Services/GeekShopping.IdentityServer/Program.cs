using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 31))));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySqlContext>()
    .AddDefaultTokenProviders();

var myIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.EmitStaticAudienceClaim = true;

})
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

myIdentityServer.AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
