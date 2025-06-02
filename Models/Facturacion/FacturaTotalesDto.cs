namespace FacturacionPortal.WEB.Models.Facturacion
{
    public class FacturaTotalesDto
    {
        public decimal SubTotal { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal BaseImpuestos { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public bool AplicaDescuento { get; set; }
    }
}
