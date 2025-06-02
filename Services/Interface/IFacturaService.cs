using FacturacionPortal.WEB.Models.Common;
using FacturacionPortal.WEB.Models.Facturacion;

namespace FacturacionPortal.WEB.Services.Interface
{
    /// <summary>
    /// Interfaz para servicios de facturación
    /// </summary>
    public interface IFacturaService
    {
        Task<(bool Success, string Message, List<FacturaDto>? Data)> GetAll();
        Task<(bool Success, string Message, FacturaDto? Data)> GetById(int id);
        Task<(bool Success, string Message, FacturaDto? Data)> GetByNumero(string numeroFactura);
        Task<(bool Success, string Message, PaginacionDto<FacturaDto>? Data)> GetPaginated(
            int page, int itemsPerPage, string? search = null, DateTime? fechaInicio = null,
            DateTime? fechaFin = null, string? estado = null, int? clienteId = null);
        Task<(bool Success, string Message, FacturaDto? Data)> Create(CrearFacturaDto facturaDto);
        Task<(bool Success, string Message, FacturaDto? Data)> Anular(int id, string motivo);
        Task<(bool Success, string Message, FacturaCalculoDto? Data)> CalcularTotales(List<CrearFacturaDetalleDto> detalles);
        Task<(bool Success, string Message, List<FacturaDto>? Data)> GetByCliente(int clienteId);
        Task<(bool Success, string Message, List<FacturaDto>? Data)> GetByFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<(bool Success, string Message, List<FacturaDetalleDto>? Data)> GetDetalles(int facturaId);
        Task<(bool Success, string Message, ReporteVentasDto? Data)> GenerarReporteVentas(DateTime fechaInicio, DateTime fechaFin);
    }
}
