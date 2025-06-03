// ----------------------------------------------------------------
// jsInterop.js - Funciones JavaScript para interoperabilidad
// ----------------------------------------------------------------

// Inicialización y configuración global
window.initJsInterop = function () {
    console.log("JsInterop inicializado correctamente");

    // Configuración del objeto navigationManager para el control de navegación
    window.navigationManager = {
        goBack: function () {
            try {
                window.history.back();
                return true;
            } catch (e) {
                console.error("Error al navegar hacia atrás:", e);
                return false;
            }
        },
        getUrl: function () {
            return window.location.href;
        },
        getQueryParam: function (name) {
            const urlParams = new URLSearchParams(window.location.search);
            return urlParams.get(name);
        }
    };

    return true;
};

// Descarga un archivo a partir de un array de bytes codificado en Base64
window.downloadFileFromBytes = function (fileName, bytesBase64, contentType) {
    const link = document.createElement('a');
    const blob = b64toBlob(bytesBase64, contentType);
    const url = URL.createObjectURL(blob);

    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

// Función auxiliar para convertir Base64 a Blob
function b64toBlob(b64Data, contentType) {
    contentType = contentType || '';
    const sliceSize = 512;
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);
        const byteNumbers = new Array(slice.length);

        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    return new Blob(byteArrays, { type: contentType });
}

// Desplazarse a un elemento específico por ID
window.scrollToElement = function (elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
        return true;
    }
    return false;
};

// Mostrar una notificación nativa de navegador (si está soportada)
window.showNotification = function (title, message) {
    if (!("Notification" in window)) {
        alert("Este navegador no soporta notificaciones de escritorio");
        return false;
    } else if (Notification.permission === "granted") {
        new Notification(title, { body: message });
        return true;
    } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(permission => {
            if (permission === "granted") {
                new Notification(title, { body: message });
                return true;
            }
        });
    }
    return false;
};