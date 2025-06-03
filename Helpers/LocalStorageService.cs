using Microsoft.JSInterop;
using System.Text.Json;

namespace FacturacionPortal.WEB.Helpers
{
    /// <summary>
    /// Servicio para manejo de LocalStorage en Blazor WebAssembly
    /// </summary>
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Obtiene un elemento del LocalStorage
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a obtener</typeparam>
        /// <param name="key">Clave del elemento</param>
        /// <returns>El objeto deserializado o null si no existe</returns>
        public async Task<T?> GetItemAsync<T>(string key)
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

                if (string.IsNullOrEmpty(json))
                    return default(T);

                if (typeof(T) == typeof(string))
                    return (T)(object)json;

                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Guarda un elemento en el LocalStorage
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a guardar</typeparam>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Valor a guardar</param>
        public async Task SetItemAsync<T>(string key, T value)
        {
            try
            {
                string json;

                if (typeof(T) == typeof(string))
                {
                    json = value?.ToString() ?? string.Empty;
                }
                else
                {
                    json = JsonSerializer.Serialize(value, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }

                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
            }
            catch (Exception)
            {
                // Error silencioso para evitar romper la aplicación
            }
        }

        /// <summary>
        /// Remueve un elemento del LocalStorage
        /// </summary>
        /// <param name="key">Clave del elemento a remover</param>
        public async Task RemoveItemAsync(string key)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (Exception)
            {
                // Error silencioso para evitar romper la aplicación
            }
        }

        /// <summary>
        /// Limpia completamente el LocalStorage
        /// </summary>
        public async Task ClearAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.clear");
            }
            catch (Exception)
            {
                // Error silencioso para evitar romper la aplicación
            }
        }

        /// <summary>
        /// Verifica si existe una clave en el LocalStorage
        /// </summary>
        /// <param name="key">Clave a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public async Task<bool> ContainKeyAsync(string key)
        {
            try
            {
                var item = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                return !string.IsNullOrEmpty(item);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de elementos en el LocalStorage
        /// </summary>
        /// <returns>Número de elementos almacenados</returns>
        public async Task<int> LengthAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<int>("eval", "localStorage.length");
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Obtiene la clave en el índice especificado
        /// </summary>
        /// <param name="index">Índice de la clave</param>
        /// <returns>La clave en el índice especificado</returns>
        public async Task<string?> KeyAsync(int index)
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.key", index);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
