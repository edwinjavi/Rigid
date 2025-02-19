using Rigid.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar HttpClient para AuthService
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://api.dtools.com"); // Cambia esta URL
});



// Configurar HttpClient para DtoolsApiService
builder.Services.AddHttpClient<IDtoolsApiService, DtoolsApiService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
