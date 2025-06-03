using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;

namespace FacturacionPortal.WEB.Helpers
{
    public class HttpInterceptor : DelegatingHandler
    {
        private readonly LocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILogger<HttpInterceptor> _logger;

        public HttpInterceptor(
            LocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider,
            ILogger<HttpInterceptor> logger)
        {
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
            _logger = logger;
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

                var response = await base.SendAsync(request, cancellationToken);

                // Manejar respuestas de error
                await HandleResponse(response);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en HttpInterceptor al procesar la solicitud");
                throw;
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