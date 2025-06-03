using System.Globalization;

namespace FacturacionPortal.WEB.Util.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly CultureInfo ColombiaCulture = new CultureInfo("es-CO");

        /// <summary>
        /// Convierte DateTime a formato colombiano corto (dd/MM/yyyy)
        /// </summary>
        /// <param name="dateTime">Fecha a formatear</param>
        /// <returns>Fecha formateada</returns>
        public static string ToColombiaShortDateString(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.FormatosFecha.FormatoCorto, ColombiaCulture);
        }

        /// <summary>
        /// Convierte DateTime a formato colombiano largo (dd/MM/yyyy HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">Fecha a formatear</param>
        /// <returns>Fecha formateada</returns>
        public static string ToColombiaLongDateString(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.FormatosFecha.FormatoLargo, ColombiaCulture);
        }

        /// <summary>
        /// Convierte DateTime a formato ISO (yyyy-MM-dd)
        /// </summary>
        /// <param name="dateTime">Fecha a formatear</param>
        /// <returns>Fecha formateada</returns>
        public static string ToISODateString(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.FormatosFecha.FormatoISO);
        }

        /// <summary>
        /// Convierte DateTime a formato de hora (HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">Fecha a formatear</param>
        /// <returns>Hora formateada</returns>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.FormatosFecha.FormatoHora);
        }

        /// <summary>
        /// Obtiene el inicio del día (00:00:00)
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Fecha con hora al inicio del día</returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Obtiene el final del día (23:59:59.999)
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Fecha con hora al final del día</returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Obtiene el inicio del mes
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Primer día del mes</returns>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Obtiene el final del mes
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Último día del mes</returns>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            return dateTime.StartOfMonth().AddMonths(1).AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// Obtiene el inicio del año
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Primer día del año</returns>
        public static DateTime StartOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// Obtiene el final del año
        /// </summary>
        /// <param name="dateTime">Fecha base</param>
        /// <returns>Último día del año</returns>
        public static DateTime EndOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31).EndOfDay();
        }

        /// <summary>
        /// Calcula la edad en años
        /// </summary>
        /// <param name="birthDate">Fecha de nacimiento</param>
        /// <returns>Edad en años</returns>
        public static int CalculateAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        /// Verifica si la fecha es hoy
        /// </summary>
        /// <param name="dateTime">Fecha a verificar</param>
        /// <returns>True si es hoy, False en caso contrario</returns>
        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        /// <summary>
        /// Verifica si la fecha es de ayer
        /// </summary>
        /// <param name="dateTime">Fecha a verificar</param>
        /// <returns>True si es ayer, False en caso contrario</returns>
        public static bool IsYesterday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// Verifica si la fecha está en el rango de una semana
        /// </summary>
        /// <param name="dateTime">Fecha a verificar</param>
        /// <returns>True si está en esta semana, False en caso contrario</returns>
        public static bool IsThisWeek(this DateTime dateTime)
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            return dateTime.Date >= startOfWeek && dateTime.Date < endOfWeek;
        }

        /// <summary>
        /// Verifica si la fecha está en el mes actual
        /// </summary>
        /// <param name="dateTime">Fecha a verificar</param>
        /// <returns>True si está en este mes, False en caso contrario</returns>
        public static bool IsThisMonth(this DateTime dateTime)
        {
            var today = DateTime.Today;
            return dateTime.Year == today.Year && dateTime.Month == today.Month;
        }

        /// <summary>
        /// Convierte DateTime nullable a string formateado
        /// </summary>
        /// <param name="dateTime">Fecha nullable</param>
        /// <param name="defaultValue">Valor por defecto si es null</param>
        /// <returns>Fecha formateada o valor por defecto</returns>
        public static string ToFormattedString(this DateTime? dateTime, string defaultValue = "-")
        {
            return dateTime?.ToColombiaShortDateString() ?? defaultValue;
        }

        /// <summary>
        /// Obtiene el nombre del mes en español
        /// </summary>
        /// <param name="dateTime">Fecha</param>
        /// <returns>Nombre del mes</returns>
        public static string GetMonthName(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM", ColombiaCulture);
        }

        /// <summary>
        /// Obtiene el nombre del día de la semana en español
        /// </summary>
        /// <param name="dateTime">Fecha</param>
        /// <returns>Nombre del día</returns>
        public static string GetDayName(this DateTime dateTime)
        {
            return dateTime.ToString("dddd", ColombiaCulture);
        }
    }
}
