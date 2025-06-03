using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FacturacionPortal.WEB.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FacturacionPortal.WEB.Helpers
{
    public class HttpInterceptor : DelegatingHandler
    {
        private readonly LocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILogger<HttpInterceptor> _logger;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public HttpInterceptor(
            LocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider,
            ILogger<HttpInterceptor> logger,
            IJSRuntime jsRuntime,
            NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
            _logger = logger;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Agregar token de autenticación si existe
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Agregar headers adicionales
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                if (!request.Headers.Contains("Accept"))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                // Intentar la solicitud HTTP
                var response = await base.SendAsync(request, cancellationToken);

                // Si es un error de autenticación, redirigir al login
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/login", true);
                    return response;
                }

                // Manejar respuestas de error
                await HandleResponse(response);

                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de conexión HTTP: {ex.Message}");

                // Crear una respuesta personalizada para los errores de conexión
                var errorResponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            exito = false,
                            mensaje = "Error de conexión con el servidor",
                            detalle = ex.Message
                        }),
                        Encoding.UTF8,
                        "application/json"
                    )
                };

                return errorResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");

                // Crear una respuesta personalizada para otros errores
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            exito = false,
                            mensaje = "Error al procesar la solicitud",
                            detalle = ex.Message
                        }),
                        Encoding.UTF8,
                        "application/json"
                    )
                };

                return errorResponse;
            }
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    await HandleUnauthorized();
                    break;
                case HttpStatusCode.Forbidden:
                    _logger.LogWarning("Acceso prohibido a recurso");
                    break;
                case HttpStatusCode.InternalServerError:
                    _logger.LogError("Error interno del servidor");
                    break;
                case HttpStatusCode.BadRequest:
                    _logger.LogWarning("Solicitud incorrecta");
                    break;
                case HttpStatusCode.NotFound:
                    _logger.LogWarning("Recurso no encontrado");
                    break;
                case HttpStatusCode.RequestTimeout:
                    _logger.LogWarning("Tiempo de espera agotado");
                    break;
            }
        }

        private async Task HandleUnauthorized()
        {
            try
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("usuarioId");

                if (_authStateProvider is JwtAuthenticationStateProvider jwtProvider)
                {
                    jwtProvider.NotifyUserLogout();
                }

                _logger.LogWarning("Usuario no autorizado, token removido");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al manejar respuesta no autorizada");
            }
        }
    }
}