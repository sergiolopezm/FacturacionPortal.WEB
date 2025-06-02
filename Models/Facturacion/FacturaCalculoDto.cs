namespace FacturacionPortal.WEB.Models.Facturacion
{
    public class FacturaCalculoDto
    {
        public List<CrearFacturaDetalleDto> Detalles { get; set; } = new List<CrearFacturaDetalleDto>();
        public FacturaTotalesDto? Totales { get; set; }
        public FacturaTotalesFormateadosDto? TotalesFormateados { get; set; }
    }
}
