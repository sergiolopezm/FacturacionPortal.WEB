﻿using System.ComponentModel.DataAnnotations;

namespace FacturacionPortal.WEB.Models.Facturacion
{
    public class CrearFacturaDetalleDto
    {
        [Required(ErrorMessage = "El artículo es requerido")]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }

        // Propiedades adicionales para validación
        public string? ArticuloCodigo { get; set; }
        public string? ArticuloNombre { get; set; }
        public int? StockDisponible { get; set; }

        // Propiedades calculadas para UI
        public decimal Subtotal => Cantidad * PrecioUnitario;
        public string SubtotalFormateado => $"${Subtotal:N0}";
        public string PrecioFormateado => $"${PrecioUnitario:N0}";
    }
}
