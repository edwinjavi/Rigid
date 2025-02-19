using Rigid.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Aqui hay que inyectar el HttpClient
// Configurar HttpClient para AuthService
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://api.dtools.com"); // 🔹 Cambia esta URL
});

// Configurar HttpClient para DtoolsApiService
builder.Services.AddHttpClient<DtoolsApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.dtools.com"); // 🔹 Cambia esta URL
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
