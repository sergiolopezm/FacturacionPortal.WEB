namespace FacturacionPortal.WEB.Util
{
    /// <summary>
    /// Tipos de alerta para el componente Alert
    /// </summary>
    public enum AlertType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    /// <summary>
    /// Tipos de confirmación para el componente Confirm
    /// </summary>
    public enum ConfirmType
    {
        Default,
        Danger,
        Warning,
        Success,
        Info
    }

    /// <summary>
    /// Tamaños para modales de confirmación
    /// </summary>
    public enum ConfirmSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    /// <summary>
    /// Acciones de confirmación
    /// </summary>
    public enum ConfirmAction
    {
        None,
        Confirm,
        Cancel
    }

    /// <summary>
    /// Tipos de spinner para el componente Loading
    /// </summary>
    public enum LoadingSpinnerType
    {
        Border,
        Grow,
        Dots,
        Pulse
    }

    /// <summary>
    /// Tamaños para el componente Loading
    /// </summary>
    public enum LoadingSize
    {
        Small,
        Medium,
        Large
    }

    /// <summary>
    /// Colores para el componente Loading
    /// </summary>
    public enum LoadingColor
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    /// <summary>
    /// Acciones de navegación
    /// </summary>
    public enum NavigationAction
    {
        None,
        Back,
        Home,
        Refresh,
        Custom
    }
}