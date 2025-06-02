namespace FacturacionPortal.WEB.Models.Auth
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
