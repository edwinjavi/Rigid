using Rigid.Models;
using Rigid.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;




//PARA ACCEDER A LA API DE DTOOLS SE DEBE ENVIAR UN TOKEN (SWAGGER UI NO DA UNA, SOLO MUESTRA UN UI COMO DICE EL NOMBRE XD
//PARA ACCEDER A SWAGGER SOLO TENES QUE CORRER LA APLICARCION Y AGREGAR /SWAGGER AL LOCALHOST

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

// 🔹 Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dtools API",
        Version = "v1",
        Description = "API de integración con Dtools CRM"
    });
});

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // 🔹 Habilitar Swagger en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dtools API v1");
    });
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

