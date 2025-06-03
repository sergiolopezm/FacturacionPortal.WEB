using FacturacionPortal.WEB.Services.Interface;
using Microsoft.JSInterop;

namespace FacturacionPortal.WEB.Services.Api
{
    /// <summary>
    /// Implementación del servicio de interoperabilidad con JavaScript
    /// </summary>
    public class JsInteropService : IJsInteropService
    {
        private readonly IJSRuntime _jsRuntime;
        private bool _initialized = false;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Inicializar()
        {
            if (!_initialized)
            {
                try
                {
                    // Inicializar funciones JS personalizadas
                    await _jsRuntime.InvokeVoidAsync("initJsInterop");
                    _initialized = true;
                }
                catch (Exception)
                {
                    // Probablemente el script aún no está cargado, es normal en pre-renderizado
                    _initialized = false;
                }
            }
        }

        public async Task MostrarAlerta(string mensaje)
        {
            await _jsRuntime.InvokeVoidAsync("alert", mensaje);
        }

        public async Task<bool> Confirmar(string mensaje)
        {
            return await _jsRuntime.InvokeAsync<bool>("confirm", mensaje);
        }

        public async Task CopiarAlPortapapeles(string texto)
        {
            await Inicializar();
            await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", texto);
        }

        public async Task DescargarArchivo(string nombreArchivo, byte[] contenido, string tipoContenido)
        {
            await Inicializar();
            await _jsRuntime.InvokeVoidAsync(
                "downloadFileFromBytes",
                nombreArchivo,
                Convert.ToBase64String(contenido),
                tipoContenido
            );
        }

        public async Task<bool> NavigateBack()
        {
            await Inicializar();
            return await _jsRuntime.InvokeAsync<bool>("navigationManager.goBack");
        }

        public async Task AbrirEnNuevaVentana(string url)
        {
            await _jsRuntime.InvokeVoidAsync("window.open", url, "_blank");
        }

        public async Task ScrollToElement(string elementId)
        {
            await Inicializar();
            await _jsRuntime.InvokeVoidAsync("scrollToElement", elementId);
        }

        public async Task ScrollToTop()
        {
            await _jsRuntime.InvokeVoidAsync("window.scrollTo", 0, 0);
        }

        public async Task ImprimirPagina()
        {
            await _jsRuntime.InvokeVoidAsync("window.print");
        }
    }
}
