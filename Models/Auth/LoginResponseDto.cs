namespace FacturacionPortal.WEB.Models.Auth
{
    /// <summary>
    /// DTO para la respuesta de login exitoso
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token JWT para autenticación
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token de actualización para renovar el token JWT
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora de expiración del token
        /// </summary>
        public DateTime Expiracion { get; set; }

        /// <summary>
        /// Información del usuario autenticado
        /// </summary>
        public UsuarioPerfilDto Usuario { get; set; } = new UsuarioPerfilDto();
    }
}
