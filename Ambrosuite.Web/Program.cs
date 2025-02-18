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

builder.Services.AddHttpClient<InventarioService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<RecetaService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<FacturacionesService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<FacturacionDetalleService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<MetodoPagosService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddHttpClient<TipoFacturasService>(client =>
{
    client.BaseAddress = new Uri(urlHttpClient); // Asegúrate de que la URL sea correcta
});

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login";    // Ruta de login
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Asegúrate de que la autenticación se ejecute antes de la autorización
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();