

using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

var sharedApplicationName = "CommonMvcAppName";
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Path.GetTempPath(), "sharedkeys", sharedApplicationName)))
    .SetApplicationName(sharedApplicationName);


builder.Services.AddAuthentication()
    .AddCookie("SharedCookie", options => options.Cookie.Name = ".AspNet.ApplicationCookie");

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer(options => ClassLibrary.RemoteServiceUtils.RegisterSessionKeys(options.KnownKeys))
    .AddWrappedAspNetCoreSession()
    .AddRemoteAppClient(options =>
{
    options.RemoteAppUrl = new("https://localhost:44389/");
    options.ApiKey = "d5bad3d1-4a61-4090-bd74-18428f96ee95";
}).AddAuthenticationClient(true)
    .AddSessionClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();
app.UseSystemWebAdapters();

try
{
    app.Run();
}
catch (Exception e)
{

    Console.WriteLine(e);
    throw;
}