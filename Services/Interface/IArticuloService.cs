using FacturacionPortal.WEB.Models.Common;
using FacturacionPortal.WEB.Models.Facturacion;

namespace FacturacionPortal.WEB.Services.Interface
{
    public interface IArticuloService
    {
        Task<(bool Success, string Message, List<ArticuloDto>? Data)> GetAll();
        Task<(bool Success, string Message, ArticuloDto? Data)> GetById(int id);
        Task<(bool Success, string Message, ArticuloDto? Data)> GetByCodigo(string codigo);
        Task<(bool Success, string Message, PaginacionDto<ArticuloDto>? Data)> GetPaginated(
            int page, int itemsPerPage, string? search = null, int? categoriaId = null);
        Task<(bool Success, string Message, ArticuloDto? Data)> Create(ArticuloDto articuloDto);
        Task<(bool Success, string Message, ArticuloDto? Data)> Update(int id, ArticuloDto articuloDto);
        Task<(bool Success, string Message, ArticuloDto? Data)> ActualizarStock(int id, int nuevoStock);
        Task<(bool Success, string Message)> Delete(int id);
        Task<(bool Success, string Message, List<ArticuloDto>? Data)> GetStockBajo();
        Task<(bool Success, string Message, List<ArticuloVendidoDto>? Data)> GetMasVendidos(
            DateTime? fechaInicio = null, DateTime? fechaFin = null, int top = 10);
        Task<(bool Success, string Message, List<ArticuloDto>? Data)> GetByCategoria(int categoriaId);
    }
}
