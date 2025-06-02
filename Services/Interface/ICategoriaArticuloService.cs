using FacturacionPortal.WEB.Models.Common;
using FacturacionPortal.WEB.Models.Facturacion;

namespace FacturacionPortal.WEB.Services.Interface
{
    public interface ICategoriaArticuloService
    {
        Task<(bool Success, string Message, List<CategoriaArticuloDto>? Data)> GetAll();
        Task<(bool Success, string Message, CategoriaArticuloDto? Data)> GetById(int id);
        Task<(bool Success, string Message, PaginacionDto<CategoriaArticuloDto>? Data)> GetPaginated(
            int page, int itemsPerPage, string? search = null);
        Task<(bool Success, string Message, CategoriaArticuloDto? Data)> Create(CategoriaArticuloDto categoriaDto);
        Task<(bool Success, string Message, CategoriaArticuloDto? Data)> Update(int id, CategoriaArticuloDto categoriaDto);
        Task<(bool Success, string Message)> Delete(int id);
        Task<(bool Success, string Message, List<ArticuloDto>? Data)> GetArticulos(int categoriaId);
        Task<(bool Success, string Message, object? Data)> ExistePorNombre(string nombre);
    }
}
