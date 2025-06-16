using ClothingApp.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClothingApp.Data;
using ClothingApp.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

var builder = WebApplication.CreateBuilder(args);


string dbFileName = "app.db"; 
string dbPath;

if (builder.Environment.IsDevelopment())
{
    dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", dbFileName);
}
else
{
    var homePath = Environment.GetEnvironmentVariable("HOME") ?? ".";
    var dataDir = Path.Combine(homePath, "data");

    if (!Directory.Exists(dataDir))
    {
        Directory.CreateDirectory(dataDir); 
    }

    dbPath = Path.Combine(dataDir, dbFileName);

    var sourcePath = Path.Combine(builder.Environment.WebRootPath, "Data", dbFileName);
    if (!File.Exists(dbPath) && File.Exists(sourcePath))
    {
        File.Copy(sourcePath, dbPath);
    }

    Console.WriteLine($"[INFO] SQLite DB Path: {dbPath}"); 
}

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddDbContextFactory<ClothingAppContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")); 

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityUserAccessor>();

builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<ClothingAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ClothingAppContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ClothingAppUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClothingAppContext>();
    db.Database.Migrate(); // Apply any pending EF Core migrations
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();;

app.Run();
