using FacturacionPortal.WEB.Helpers;
using FacturacionPortal.WEB.Models.Auth;
using FacturacionPortal.WEB.Services.Interface;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FacturacionPortal.WEB.Services.Api
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly LocalStorageService _localStorage;
        private readonly IJSRuntime _jsRuntime;
        private readonly JwtAuthenticationStateProvider _authStateProvider;
        private UsuarioPerfilDto? _currentUser;

        public AuthService(
            HttpClient httpClient,
            LocalStorageService localStorage,
            IJSRuntime jsRuntime,
            JwtAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
            
            // Para depuración: imprimir la URL base
            Console.WriteLine($"Auth Service - Base URL: {_httpClient.BaseAddress}");
        }

        public async Task<(bool Success, string? Message, LoginResponseDto? Data)> Login(UsuarioLoginDto model)
        {
            try
            {
                // Verificar conexión a internet o servidor antes de intentar
                if (!await IsServerReachable())
                {
                    return (false, "No se pudo establecer conexión con el servidor. Verifique su conexión a internet o que el servidor esté disponible.", null);
                }

                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString();

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<LoginResponseDto>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (resultado != null)
                        {
                            // Almacenar el token en el localStorage
                            await _localStorage.SetItemAsync("authToken", resultado.Token);
                            await _localStorage.SetItemAsync("refreshToken", resultado.RefreshToken);
                            
                            // Guardar el ID del usuario en localStorage
                            await _localStorage.SetItemAsync("usuarioId", resultado.Usuario.Id.ToString());

                            // Notificar al proveedor de autenticación que el estado ha cambiado
                            var authStateProvider = _authStateProvider as JwtAuthenticationStateProvider;
                            authStateProvider?.NotifyUserAuthentication(resultado.Token);

                            return (true, mensaje, resultado);
                        }
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    string errorMessage;
                    try
                    {
                        // Intentar leer el mensaje de error desde la API
                        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                        errorMessage = jsonResponse.GetProperty("mensaje").GetString() ?? "Error de autenticación";
                    }
                    catch
                    {
                        // Si no se puede deserializar, usar un mensaje genérico según el código de estado
                        errorMessage = response.StatusCode switch
                        {
                            HttpStatusCode.BadRequest => "Los datos proporcionados no son válidos",
                            HttpStatusCode.Unauthorized => "Credenciales incorrectas",
                            HttpStatusCode.Forbidden => "No tiene permisos para acceder",
                            _ => $"Error en la autenticación ({(int)response.StatusCode})"
                        };
                    }
                    return (false, errorMessage, null);
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar específicamente errores de conexión HTTP
                string errorMessage = "Error de conexión con el servidor";
                if (ex.InnerException != null)
                {
                    errorMessage += $": {ex.InnerException.Message}";
                }
                return (false, errorMessage, null);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción
                return (false, $"Error: {ex.Message}", null);
            }
        }

        private async Task<bool> IsServerReachable()
        {
            try
            {
                Console.WriteLine("Verificando conexión con servidor...");
                Console.WriteLine($"URL base: {_httpClient.BaseAddress}");
                
                // Modificar el endpoint de verificación o probar con cualquier endpoint
                // que se sepa que existe en la API
                var request = new HttpRequestMessage(HttpMethod.Head, "");
                Console.WriteLine($"Enviando request HEAD a: {_httpClient.BaseAddress}");
                
                var response = await _httpClient.SendAsync(request);
                Console.WriteLine($"Respuesta del servidor: {response.StatusCode}");
                
                // Incluso un 404 significa que el servidor está activo
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar servidor: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Excepción interna: {ex.InnerException.Message}");
                }
                return false;
            }
        }

        public async Task<(bool Success, string Message, UsuarioPerfilDto? Data)> GetProfile()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(token))
                {
                    return (false, "No ha iniciado sesión", null);
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync("api/Auth/perfil");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Perfil obtenido";

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<UsuarioPerfilDto>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        _currentUser = resultado;
                        return (true, mensaje, resultado);
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error obteniendo perfil";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> Logout()
        {
            try
            {
                // Limpiar datos de autenticación usando JavaScript
                await _jsRuntime.InvokeVoidAsync("authService.clearAuthData");

                var token = await _localStorage.GetItemAsync<string>("authToken");
                
                // Eliminar el token del localStorage sin importar el resultado de la API
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                await _localStorage.RemoveItemAsync("usuarioId");
                _currentUser = null;
                
                _authStateProvider.NotifyUserLogout();
                
                // Solo llamar a la API si hay token
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var response = await _httpClient.PostAsync("api/Auth/logout", null);
                        
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                            var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Sesión cerrada exitosamente";
                            return (true, mensaje);
                        }
                    }
                    catch
                    {
                        // Ignorar errores de la API al cerrar sesión
                    }
                }
                
                return (true, "Sesión cerrada localmente");
            }
            catch (Exception ex)
            {
                // En caso de error, asegurarnos de eliminar los tokens localmente
                try
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("refreshToken");
                    await _localStorage.RemoveItemAsync("usuarioId");
                    _currentUser = null;
                    _authStateProvider.NotifyUserLogout();
                }
                catch { }
                
                return (true, "Sesión cerrada con errores: " + ex.Message);
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(token))
                    return false;
                
                // Verificar que el token no esté vencido
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var expiration = jwtToken.ValidTo;
                
                // Si el token ha expirado, eliminarlo
                if (expiration < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("refreshToken");
                    await _localStorage.RemoveItemAsync("usuarioId");
                    return false;
                }
                
                return true;
            }
            catch
            {
                // Si hay error al leer el token (formato inválido), eliminarlo
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                await _localStorage.RemoveItemAsync("usuarioId");
                return false;
            }
        }

        public UsuarioPerfilDto? GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
