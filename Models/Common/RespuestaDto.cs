namespace FacturacionPortal.WEB.Models.Common
{
    /// <summary>
    /// Clase para estandarizar las respuestas de la API
    /// </summary>
    public class RespuestaDto
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
        public string? Detalle { get; set; }
        public object? Resultado { get; set; }
    }
}
