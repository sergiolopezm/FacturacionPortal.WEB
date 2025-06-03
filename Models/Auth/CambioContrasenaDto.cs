using System.ComponentModel.DataAnnotations;

namespace FacturacionPortal.WEB.Models.Auth
{
    public class CambioContrasenaDto
    {
        [Required(ErrorMessage = "La contraseña actual es requerida")]
        public string ContraseñaActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string NuevaContraseña { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la nueva contraseña")]
        [Compare("NuevaContraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContraseña { get; set; } = string.Empty;
    }
}
