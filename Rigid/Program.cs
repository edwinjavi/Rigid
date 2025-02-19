using Rigid.Models;
using Rigid.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar configuración
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Registrar servicios personalizados
builder.Services.AddSingleton<AuthService>();

// Configurar HttpClient para AuthService
builder.Services.AddHttpClient<AuthService>(client =>
{
    var apiSettings = builder.Configuration.GetSection("ApiSettings");
    var dtoolsApiKey = apiSettings["DtoolsApiKey"];
    if (string.IsNullOrEmpty(dtoolsApiKey))
    {
        throw new InvalidOperationException("La clave ApiSettings:DtoolsApiKey no está configurada en appsettings.json.");
    }
    client.BaseAddress = new Uri(dtoolsApiKey);
});

// Configurar HttpClient para DtoolsApiService
builder.Services.AddHttpClient<IDtoolsApiService, DtoolsApiService>();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilitar CORS
app.UseCors("AllowAll");

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configurar las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
