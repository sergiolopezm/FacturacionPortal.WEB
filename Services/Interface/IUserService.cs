using FacturacionPortal.WEB.Models.Auth;

namespace FacturacionPortal.WEB.Services.Interface
{
    /// <summary>
    /// Interfaz para servicios de gestión de usuarios
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        Task<(bool Success, string Message, IEnumerable<UsuarioPerfilDto>? Data)> GetAllUsers();

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        Task<(bool Success, string Message, UsuarioPerfilDto? Data)> GetUserById(Guid id);

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        Task<(bool Success, string Message, UsuarioPerfilDto? Data)> CreateUser(UsuarioCreacionDto usuarioDto);

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        Task<(bool Success, string Message, UsuarioPerfilDto? Data)> UpdateUser(Guid id, UsuarioActualizacionDto usuarioDto);

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        Task<(bool Success, string Message)> ChangePassword(Guid id, CambioContrasenaDto cambioDto);

        /// <summary>
        /// Activa o desactiva un usuario
        /// </summary>
        Task<(bool Success, string Message)> ToggleUserStatus(Guid id, bool activar);
    }
}
