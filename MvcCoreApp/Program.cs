using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder();
//builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// These must match the data protection settings in MvcApp Startup.Auth.cs for cookie sharing to work
var sharedApplicationName = "CommonMvcAppName";
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Path.GetTempPath(), "sharedkeys", sharedApplicationName)))
    .SetApplicationName(sharedApplicationName);

builder.Services.AddAuthentication()
    .AddCookie("SharedCookie", options => options.Cookie.Name = ".AspNet.ApplicationCookie");

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer(options => ClassLibrary.RemoteServiceUtils.RegisterSessionKeys(options.KnownKeys))
    .AddRemoteAppClient(options =>
    {
        options.RemoteAppUrl = new("https://localhost:44339/");
        options.ApiKey = builder.Configuration["RemoteAppApiKey"];
    })
    .AddAuthenticationClient(true)
    .AddSessionClient();

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

app.UseSystemWebAdapters();

app.UseEndpoints(endpoints =>
{
    app.MapDefaultControllerRoute();
    // This method can be used to enable session (or read-only session) on all controllers
    //.RequireSystemWebAdapterSession();

    //   app.MapReverseProxy();
});

app.Run();
