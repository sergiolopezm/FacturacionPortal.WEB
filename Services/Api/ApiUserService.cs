using FacturacionPortal.WEB.Models.Auth;
using FacturacionPortal.WEB.Services.Interface;
using System.Net.Http.Json;
using System.Text.Json;

namespace FacturacionPortal.WEB.Services.Api
{
    public class ApiUserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public ApiUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool Success, string Message, IEnumerable<UsuarioPerfilDto>? Data)> GetAllUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Usuarios");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Usuarios obtenidos";

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<IEnumerable<UsuarioPerfilDto>>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return (true, mensaje, resultado);
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error al obtener usuarios";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, UsuarioPerfilDto? Data)> GetUserById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Usuarios/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Usuario obtenido";

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<UsuarioPerfilDto>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return (true, mensaje, resultado);
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error al obtener usuario";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, UsuarioPerfilDto? Data)> CreateUser(UsuarioCreacionDto usuarioDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Usuarios", usuarioDto);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Usuario creado exitosamente";

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<UsuarioPerfilDto>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return (true, mensaje, resultado);
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error al crear usuario";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, UsuarioPerfilDto? Data)> UpdateUser(Guid id, UsuarioActualizacionDto usuarioDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Usuarios/{id}", usuarioDto);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Usuario actualizado exitosamente";

                    if (exito)
                    {
                        var resultadoJson = jsonResponse.GetProperty("resultado").ToString();
                        var resultado = JsonSerializer.Deserialize<UsuarioPerfilDto>(resultadoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return (true, mensaje, resultado);
                    }

                    return (false, mensaje, null);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error al actualizar usuario";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> ChangePassword(Guid id, CambioContrasenaDto cambioDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Usuarios/{id}/cambio-contrasena", cambioDto);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Contraseña actualizada exitosamente";

                    return (exito, mensaje);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? "Error al cambiar contraseña";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> ToggleUserStatus(Guid id, bool activar)
        {
            try
            {
                var accion = activar ? "activar" : "desactivar";
                var response = await _httpClient.PostAsync($"api/Usuarios/{id}/{accion}", null);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var exito = jsonResponse.GetProperty("exito").GetBoolean();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? $"Usuario {(activar ? "activado" : "desactivado")} exitosamente";

                    return (exito, mensaje);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var mensaje = jsonResponse.GetProperty("mensaje").GetString() ?? $"Error al {accion} usuario";
                    var detalle = jsonResponse.GetProperty("detalle").GetString() ?? "";
                    return (false, $"{mensaje}: {detalle}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
