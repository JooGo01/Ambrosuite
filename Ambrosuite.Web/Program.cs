using Ambrosuite.Web;
using Ambrosuite.Web.Components;
using Ambrosuite.Web.ServicesWeb;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Kestrel explícita para producción con certificados
/*
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5002); // solo HTTP
});
*/

// Config Aspire / Componentes Razor
builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

//Configurar la base de la API (podés actualizar IP/puerto si cambia)
string urlHttpClient = "http://168.231.92.249:5000";

builder.Services.AddHttpClient<MesasService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<PedidosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<PedidoDetalleService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<UsuariosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<RolesService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<CategoriaGastosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<CategoriaProductosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<CategoriasService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<IngredientesService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<ProductosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<InventarioService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<RecetaService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<FacturacionesService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<FacturacionDetalleService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<AuthService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<MetodoPagosService>(client => client.BaseAddress = new Uri(urlHttpClient));
builder.Services.AddHttpClient<TipoFacturasService>(client => client.BaseAddress = new Uri(urlHttpClient));

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

//Middleware de seguridad solo si no estás en desarrollo
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}




//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapDefaultEndpoints();

app.Run();