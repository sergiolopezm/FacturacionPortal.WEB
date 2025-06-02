using FacturacionPortal.WEB.Models.Facturacion;

namespace FacturacionPortal.WEB.Services.Interface
{
    public interface IReporteService
    {
        Task<(bool Success, string Message, ReporteVentasDto? Data)> GenerarReporteVentas(DateTime fechaInicio, DateTime fechaFin);
        Task<(bool Success, string Message, List<ArticuloVendidoDto>? Data)> GetArticulosMasVendidos(
            DateTime fechaInicio, DateTime fechaFin, int top = 10);
        Task<(bool Success, string Message, List<ClienteFrecuenteDto>? Data)> GetClientesFrecuentes(
            DateTime fechaInicio, DateTime fechaFin, int top = 10);
        Task<(bool Success, string Message, object? Data)> GetVentasPorMes(int año);
        Task<(bool Success, string Message, object? Data)> GetFacturasPorEstado();
        Task<(bool Success, string Message, object? Data)> GetVentasPorCategoria(DateTime fechaInicio, DateTime fechaFin);
        Task<(bool Success, string Message, object? Data)> GetVentasDelDia(DateTime? fecha = null);
        Task<(bool Success, string Message, object? Data)> GetPromedioVentasDiarias(DateTime fechaInicio, DateTime fechaFin);
        Task<(bool Success, string Message, List<ArticuloDto>? Data)> GetArticulosStockBajo();
        Task<(bool Success, string Message, object? Data)> GetDashboard();
    }
}
