using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FacturacionPortal.WEB.Util
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Valida un objeto usando DataAnnotations
        /// </summary>
        /// <param name="obj">Objeto a validar</param>
        /// <returns>Lista de errores de validación</returns>
        public static List<ValidationResult> ValidateObject(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);
            Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return validationResults;
        }

        /// <summary>
        /// Valida si un objeto es válido según DataAnnotations
        /// </summary>
        /// <param name="obj">Objeto a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            return Validator.TryValidateObject(obj, validationContext, null, true);
        }

        /// <summary>
        /// Valida formato de email
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }

        /// <summary>
        /// Valida número de documento colombiano
        /// </summary>
        /// <param name="numeroDocumento">Número de documento a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidDocumento(string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento))
                return false;

            // Solo números y guiones para NIT
            var regex = new Regex(@"^[0-9\-]+$");
            return regex.IsMatch(numeroDocumento) && numeroDocumento.Length >= 6 && numeroDocumento.Length <= 15;
        }

        /// <summary>
        /// Valida formato de teléfono colombiano
        /// </summary>
        /// <param name="telefono">Teléfono a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            // Acepta números, espacios, guiones y paréntesis
            var regex = new Regex(@"^[\d\s\-\(\)\+]+$");
            return regex.IsMatch(telefono) && telefono.Length >= 7 && telefono.Length <= 20;
        }

        /// <summary>
        /// Valida que un precio sea válido
        /// </summary>
        /// <param name="precio">Precio a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidPrecio(decimal precio)
        {
            return precio > 0 && precio <= 999999999.99m;
        }

        /// <summary>
        /// Valida que una cantidad sea válida
        /// </summary>
        /// <param name="cantidad">Cantidad a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidCantidad(int cantidad)
        {
            return cantidad > 0 && cantidad <= 999999;
        }

        /// <summary>
        /// Valida que un stock sea válido
        /// </summary>
        /// <param name="stock">Stock a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidStock(int stock)
        {
            return stock >= 0 && stock <= 999999;
        }

        /// <summary>
        /// Valida código de artículo
        /// </summary>
        /// <param name="codigo">Código a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidCodigoArticulo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            // Letras, números y algunos caracteres especiales
            var regex = new Regex(@"^[A-Za-z0-9\-_\.]+$");
            return regex.IsMatch(codigo) && codigo.Length >= 1 && codigo.Length <= 20;
        }

        /// <summary>
        /// Valida que una fecha esté en un rango válido
        /// </summary>
        /// <param name="fecha">Fecha a validar</param>
        /// <param name="fechaMinima">Fecha mínima permitida</param>
        /// <param name="fechaMaxima">Fecha máxima permitida</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidFecha(DateTime fecha, DateTime? fechaMinima = null, DateTime? fechaMaxima = null)
        {
            var min = fechaMinima ?? new DateTime(1900, 1, 1);
            var max = fechaMaxima ?? DateTime.Now.AddYears(10);

            return fecha >= min && fecha <= max;
        }

        /// <summary>
        /// Valida rango de fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return fechaInicio <= fechaFin &&
                   fechaInicio >= new DateTime(2000, 1, 1) &&
                   fechaFin <= DateTime.Now.AddDays(1);
        }

        /// <summary>
        /// Valida nombre de usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return false;

            // Solo letras, números y guión bajo
            var regex = new Regex(@"^[A-Za-z0-9_]+$");
            return regex.IsMatch(nombreUsuario) &&
                   nombreUsuario.Length >= Constants.Validacion.NombreUsuarioMinLength &&
                   nombreUsuario.Length <= Constants.LongitudesCampos.NombreUsuarioMaxLength;
        }

        /// <summary>
        /// Valida fortaleza de contraseña
        /// </summary>
        /// <param name="contraseña">Contraseña a validar</param>
        /// <returns>True si es válida, False en caso contrario</returns>
        public static bool IsValidContraseña(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña))
                return false;

            return contraseña.Length >= Constants.Validacion.ContraseñaMinLength &&
                   contraseña.Length <= Constants.LongitudesCampos.ContraseñaMaxLength;
        }

        /// <summary>
        /// Sanitiza una cadena removiendo caracteres peligrosos
        /// </summary>
        /// <param name="input">Cadena a sanitizar</param>
        /// <returns>Cadena sanitizada</returns>
        public static string SanitizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remover caracteres peligrosos para HTML/Script
            return input.Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;")
                       .Replace("'", "&#x27;")
                       .Replace("&", "&amp;")
                       .Trim();
        }

        /// <summary>
        /// Obtiene mensajes de error de validación
        /// </summary>
        /// <param name="validationResults">Resultados de validación</param>
        /// <returns>Lista de mensajes de error</returns>
        public static List<string> GetErrorMessages(List<ValidationResult> validationResults)
        {
            return validationResults.Select(vr => vr.ErrorMessage ?? "Error de validación").ToList();
        }
    }
}
