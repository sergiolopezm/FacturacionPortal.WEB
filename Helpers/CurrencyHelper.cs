using System.Globalization;

namespace FacturacionPortal.WEB.Helpers
{
    public static class CurrencyHelper
    {
        private static readonly CultureInfo ColombiaCulture = new CultureInfo("es-CO");

        /// <summary>
        /// Formatea un valor decimal como moneda colombiana
        /// </summary>
        /// <param name="amount">Valor a formatear</param>
        /// <returns>Valor formateado como $1.234.567</returns>
        public static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C0", ColombiaCulture);
        }

        /// <summary>
        /// Formatea un valor decimal como moneda colombiana sin símbolo
        /// </summary>
        /// <param name="amount">Valor a formatear</param>
        /// <returns>Valor formateado como 1.234.567</returns>
        public static string FormatNumber(decimal amount)
        {
            return amount.ToString("N0", ColombiaCulture);
        }

        /// <summary>
        /// Formatea un valor decimal como moneda con decimales
        /// </summary>
        /// <param name="amount">Valor a formatear</param>
        /// <returns>Valor formateado como $1.234.567,89</returns>
        public static string FormatCurrencyWithDecimals(decimal amount)
        {
            return amount.ToString("C2", ColombiaCulture);
        }

        /// <summary>
        /// Formatea un valor decimal como número con decimales
        /// </summary>
        /// <param name="amount">Valor a formatear</param>
        /// <returns>Valor formateado como 1.234.567,89</returns>
        public static string FormatNumberWithDecimals(decimal amount)
        {
            return amount.ToString("N2", ColombiaCulture);
        }

        /// <summary>
        /// Convierte una cadena a decimal de forma segura
        /// </summary>
        /// <param name="value">Valor a convertir</param>
        /// <returns>Valor decimal o 0 si no se puede convertir</returns>
        public static decimal ParseCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            // Remover símbolos de moneda y espacios
            value = value.Replace("$", "").Replace(" ", "").Trim();

            if (decimal.TryParse(value, NumberStyles.Currency, ColombiaCulture, out decimal result))
                return result;

            if (decimal.TryParse(value, NumberStyles.Number, ColombiaCulture, out result))
                return result;

            return 0;
        }

        /// <summary>
        /// Valida si una cadena representa un valor monetario válido
        /// </summary>
        /// <param name="value">Valor a validar</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        public static bool IsValidCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Replace("$", "").Replace(" ", "").Trim();

            return decimal.TryParse(value, NumberStyles.Currency, ColombiaCulture, out _) ||
                   decimal.TryParse(value, NumberStyles.Number, ColombiaCulture, out _);
        }

        /// <summary>
        /// Calcula el porcentaje de un valor
        /// </summary>
        /// <param name="amount">Valor base</param>
        /// <param name="percentage">Porcentaje a calcular</param>
        /// <returns>Valor del porcentaje</returns>
        public static decimal CalculatePercentage(decimal amount, decimal percentage)
        {
            return Math.Round(amount * (percentage / 100), 2);
        }

        /// <summary>
        /// Redondea un valor monetario a 2 decimales
        /// </summary>
        /// <param name="amount">Valor a redondear</param>
        /// <returns>Valor redondeado</returns>
        public static decimal RoundCurrency(decimal amount)
        {
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Convierte un valor a formato de entrada (sin formateo)
        /// </summary>
        /// <param name="amount">Valor a convertir</param>
        /// <returns>Valor como string sin formato</returns>
        public static string ToInputFormat(decimal amount)
        {
            return amount.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}
