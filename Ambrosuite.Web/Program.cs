using Ambrosuite.Web.Components;
using Ambrosuite.Web;
using Ambrosuite.Web.ServicesWeb;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

String urlHttpClient = "https://localhost:5001";

builder.Services.AddHttpClient<MesasService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<PedidosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<PedidoDetalleService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<UsuariosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<RolesService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<CategoriaGastosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<CategoriaProductosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<CategoriasService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<IngredientesService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<ProductosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});
// Configura Kestrel para escuchar en los puertos deseados
//builder.WebHost.UseUrls("https://localhost:2215", "http://localhost:2216");
/*
builder.WebHost.ConfigureKestrel(options =>
{
    // Puerto HTTPS
    options.ListenAnyIP(2215, listenOptions => listenOptions.UseHttps());  // HTTPS en el puerto 2215
    // Puerto HTTP
    options.ListenAnyIP(2216);  // HTTP en el puerto 2216
});
*/

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
