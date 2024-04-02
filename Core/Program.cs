var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSystemWebAdapters()
    .AddJsonSessionSerializer(options =>
    {
        options.RegisterKey<string>("crumbs");
        options.RegisterKey<string>("language");
    })
    .AddRemoteAppClient(options =>
    {
        options.RemoteAppUrl = new("http://localhost:57806/");
        options.ApiKey = builder.Configuration.GetValue<string>("RemoteAppApiKey");
    })
    .AddSessionClient()
    .AddAuthenticationClient(true)
    ;
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

app.Run();