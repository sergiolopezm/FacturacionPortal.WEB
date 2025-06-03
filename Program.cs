// -----------------------------------------------------------------------------
// FacturacionPortal.WEB / Program.cs
// -----------------------------------------------------------------------------
using FacturacionPortal.WEB;
using FacturacionPortal.WEB.Helpers;
using FacturacionPortal.WEB.Services;
using FacturacionPortal.WEB.Services.Api;
using FacturacionPortal.WEB.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 0) CREAR EL HOST BUILDER
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 1) HTTPCLIENT PARA RECURSOS ESTÁTICOS
//    (ruta del propio sitio en https://localhost:xxxx/)
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 2) CARGAR URL DE LA API DESDE appsettings.json
//    (quedará embebido en wwwroot gracias al <ItemGroup> del .csproj)
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
Console.WriteLine($"»  Cargando configuración desde: {builder.HostEnvironment.BaseAddress}appsettings.json");

var apiUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
if (string.IsNullOrWhiteSpace(apiUrl))
{
    apiUrl = builder.HostEnvironment.BaseAddress;                // Fallback a la misma URL base
    Console.WriteLine($"»»  ApiSettings:BaseUrl no encontrado. Usando {apiUrl}");
}
else
{
    Console.WriteLine($"»  ApiSettings:BaseUrl = {apiUrl}");
}
if (!apiUrl.EndsWith("/")) apiUrl += "/";

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 3) LOGGING (en navegador aparece en consola)
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
builder.Services.AddLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Information);
    logging.AddFilter("Microsoft", LogLevel.Warning);
    logging.AddFilter("System", LogLevel.Warning);
    logging.AddFilter("FacturacionPortal.WEB", LogLevel.Debug);
});

// Registro de excepciones no manejadas (útil en desarrollo)
AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    Console.Error.WriteLine($"»»  Error no manejado: {e.ExceptionObject}");
    Debug.WriteLine($"»»  Error no manejado: {e.ExceptionObject}");
};

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 4) SERVICIOS AUXILIARES + AUTH
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 5) INTERCEPTOR HTTP + CONFIGURACIÓN COMÚN
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
builder.Services.AddScoped<HttpInterceptor>();

// Delegado que aplicaremos a todos los HttpClient tipados
Action<HttpClient> configureClient = client =>
{
    client.BaseAddress = new Uri(apiUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
};

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// 6) CLIENTES TIPADOS (API)
//    Cada cliente hereda el HttpInterceptor ➜ manejo de errores centralizado
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
builder.Services.AddHttpClient<IAuthService, AuthService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<IArticuloService, ApiArticuloService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<ICategoriaArticuloService, ApiCategoriaArticuloService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<IClienteService, ApiClienteService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<IFacturaService, ApiFacturaService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<IReporteService, ApiReporteService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

builder.Services.AddHttpClient<IUserService, ApiUserService>(configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

// Registrar servicio de interoperabilidad JS
builder.Services.AddScoped<IJsInteropService, JsInteropService>();

// HttpClient "genérico" por si necesitas inyectar IHttpClientFactory directamente
builder.Services.AddHttpClient("API", configureClient)
                .AddHttpMessageHandler<HttpInterceptor>();

// Registrar la URL por si algún otro servicio la requiere
builder.Services.AddSingleton(new ApiUrlConfig { BaseUrl = apiUrl });

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
await builder.Build().RunAsync();

// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
// Clase auxiliar que permite inyectar la URL base de la API en cualquier clase
// ¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤
public class ApiUrlConfig
{
    public string BaseUrl { get; set; } = string.Empty;
}