using FacturacionPortal.WEB.Models.Common;
using FacturacionPortal.WEB.Models.Facturacion;

namespace FacturacionPortal.WEB.Services.Interface
{
    public interface IClienteService
    {
        Task<(bool Success, string Message, List<ClienteDto>? Data)> GetAll();
        Task<(bool Success, string Message, ClienteDto? Data)> GetById(int id);
        Task<(bool Success, string Message, ClienteDto? Data)> GetByDocumento(string numeroDocumento);
        Task<(bool Success, string Message, PaginacionDto<ClienteDto>? Data)> GetPaginated(
            int page, int itemsPerPage, string? search = null);
        Task<(bool Success, string Message, ClienteDto? Data)> Create(ClienteDto clienteDto);
        Task<(bool Success, string Message, ClienteDto? Data)> Update(int id, ClienteDto clienteDto);
        Task<(bool Success, string Message)> Delete(int id);
        Task<(bool Success, string Message, List<ClienteFrecuenteDto>? Data)> GetFrecuentes(int top = 10);
    }
}
