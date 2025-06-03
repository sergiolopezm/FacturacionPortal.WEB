namespace FacturacionPortal.WEB.Services.Interface
{
    /// <summary>
    /// Interfaz para servicios de interoperabilidad con JavaScript
    /// </summary>
    public interface IJsInteropService
    {
        /// <summary>
        /// Muestra una alerta en el navegador
        /// </summary>
        Task MostrarAlerta(string mensaje);

        /// <summary>
        /// Muestra un cuadro de confirmación en el navegador
        /// </summary>
        Task<bool> Confirmar(string mensaje);

        /// <summary>
        /// Copia un texto al portapapeles
        /// </summary>
        Task CopiarAlPortapapeles(string texto);

        /// <summary>
        /// Descarga un archivo como blob
        /// </summary>
        Task DescargarArchivo(string nombreArchivo, byte[] contenido, string tipoContenido);

        /// <summary>
        /// Navega hacia atrás en el historial del navegador
        /// </summary>
        Task<bool> NavigateBack();

        /// <summary>
        /// Abre una URL en una nueva ventana
        /// </summary>
        Task AbrirEnNuevaVentana(string url);

        /// <summary>
        /// Inicializa funciones JS requeridas
        /// </summary>
        Task Inicializar();

        /// <summary>
        /// Desplaza la pantalla al elemento con el ID proporcionado
        /// </summary>
        Task ScrollToElement(string elementId);

        /// <summary>
        /// Desplaza la pantalla a la parte superior
        /// </summary>
        Task ScrollToTop();

        /// <summary>
        /// Imprime la página actual
        /// </summary>
        Task ImprimirPagina();
    }
}