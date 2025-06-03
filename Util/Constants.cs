namespace FacturacionPortal.WEB.Util
{
    public static class Constants
    {
        // Estados de Factura
        public static class EstadosFactura
        {
            public const string Activa = "Activa";
            public const string Anulada = "Anulada";
            public const string Pagada = "Pagada";
            public const string Pendiente = "Pendiente";
        }

        // Configuración de Facturación
        public static class Facturacion
        {
            public const decimal PorcentajeIVA = 19m;
            public const decimal PorcentajeDescuento = 5m;
            public const decimal MontoMinimoDescuento = 500000m;
            public const int StockMinimoDefault = 5;
        }

        // Paginación
        public static class Paginacion
        {
            public const int ElementosPorPaginaDefault = 10;
            public const int ElementosPorPaginaMaximo = 100;
            public static readonly int[] OpcionesPorPagina = { 5, 10, 15, 20, 25, 50 };
        }

        // Longitudes de campos
        public static class LongitudesCampos
        {
            public const int NumeroDocumentoMaxLength = 15;
            public const int NombresMaxLength = 100;
            public const int ApellidosMaxLength = 100;
            public const int DireccionMaxLength = 250;
            public const int TelefonoMaxLength = 20;
            public const int EmailMaxLength = 100;
            public const int CodigoArticuloMaxLength = 20;
            public const int NombreArticuloMaxLength = 100;
            public const int DescripcionMaxLength = 250;
            public const int ObservacionesMaxLength = 500;
            public const int NombreUsuarioMaxLength = 100;
            public const int ContraseñaMaxLength = 50;
            public const int NombreCategoriaMaxLength = 100;
        }

        // Mensajes del sistema
        public static class Mensajes
        {
            // Éxito
            public const string FacturaCreada = "Factura creada exitosamente";
            public const string FacturaAnulada = "Factura anulada exitosamente";
            public const string ClienteCreado = "Cliente creado exitosamente";
            public const string ClienteActualizado = "Cliente actualizado exitosamente";
            public const string ClienteEliminado = "Cliente eliminado exitosamente";
            public const string ArticuloCreado = "Artículo creado exitosamente";
            public const string ArticuloActualizado = "Artículo actualizado exitosamente";
            public const string ArticuloEliminado = "Artículo eliminado exitosamente";
            public const string CategoriaCreada = "Categoría creada exitosamente";
            public const string CategoriaActualizada = "Categoría actualizada exitosamente";
            public const string CategoriaEliminada = "Categoría eliminada exitosamente";

            // Errores
            public const string ErrorGeneral = "Ha ocurrido un error inesperado";
            public const string ErrorConexion = "Error de conexión con el servidor";
            public const string ErrorAutenticacion = "Error de autenticación";
            public const string FacturaNoEncontrada = "Factura no encontrada";
            public const string ClienteNoEncontrado = "Cliente no encontrado";
            public const string ArticuloNoEncontrado = "Artículo no encontrado";
            public const string CategoriaNoEncontrada = "Categoría no encontrada";
            public const string StockInsuficiente = "Stock insuficiente para el artículo";
            public const string DatosRequeridos = "Todos los campos obligatorios deben ser completados";

            // Confirmaciones
            public const string ConfirmarEliminar = "¿Está seguro que desea eliminar este registro?";
            public const string ConfirmarAnular = "¿Está seguro que desea anular esta factura?";
        }

        // Roles del sistema
        public static class Roles
        {
            public const string Administrador = "Administrador";
            public const string Vendedor = "Vendedor";
            public const string Consultor = "Consultor";
        }

        // Tipos de documento
        public static class TiposDocumento
        {
            public const string CedulaCiudadania = "CC";
            public const string CedulaExtranjeria = "CE";
            public const string Pasaporte = "PA";
            public const string NIT = "NIT";
        }

        // Formatos de fecha
        public static class FormatosFecha
        {
            public const string FormatoCorto = "dd/MM/yyyy";
            public const string FormatoLargo = "dd/MM/yyyy HH:mm:ss";
            public const string FormatoISO = "yyyy-MM-dd";
            public const string FormatoHora = "HH:mm:ss";
        }

        // Configuración de reportes
        public static class Reportes
        {
            public const int TopArticulosMasVendidos = 10;
            public const int TopClientesFrecuentes = 10;
            public const int DiasReporteDefault = 30;
        }

        // Configuración de validación
        public static class Validacion
        {
            public const int NombreUsuarioMinLength = 3;
            public const int ContraseñaMinLength = 6;
            public const decimal PrecioMinimoArticulo = 0.01m;
            public const int CantidadMinima = 1;
            public const int StockMinimo = 0;
        }

        // CSS Classes
        public static class CssClasses
        {
            public const string BtnPrimary = "btn btn-primary";
            public const string BtnSecondary = "btn btn-secondary";
            public const string BtnSuccess = "btn btn-success";
            public const string BtnDanger = "btn btn-danger";
            public const string BtnWarning = "btn btn-warning";
            public const string BtnInfo = "btn btn-info";
            public const string AlertSuccess = "alert alert-success";
            public const string AlertDanger = "alert alert-danger";
            public const string AlertWarning = "alert alert-warning";
            public const string AlertInfo = "alert alert-info";
        }

        // Local Storage Keys
        public static class LocalStorageKeys
        {
            public const string AuthToken = "authToken";
            public const string UsuarioId = "usuarioId";
            public const string ConfiguracionUsuario = "configuracionUsuario";
        }
    }
}
