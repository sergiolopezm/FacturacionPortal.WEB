// Funcionalidad para obtener información del cliente
window.clientInfo = {
    // Obtiene la dirección IP del cliente (simulada para WebAssembly)
    getIpAddress: function () {
        // En Blazor WebAssembly no es posible obtener la IP real del cliente desde el navegador
        // Esto es solo una simulación para mantener el flujo de trabajo
        return "127.0.0.1";
    },

    // Obtiene información básica del navegador
    getBrowserInfo: function () {
        return {
            userAgent: navigator.userAgent,
            language: navigator.language,
            platform: navigator.platform
        };
    }
};