/**
 * Módulo de navegación para aplicación Blazor
 */
window.navigationManager = {
    /**
     * Navega hacia atrás en el historial del navegador
     * @returns {boolean} - Devuelve true si la navegación fue exitosa
     */
    goBack: function () {
        try {
            history.back();
            return true;
        } catch (e) {
            console.error("Error al navegar hacia atrás:", e);
            return false;
        }
    },

    /**
     * Recarga la página actual
     * @returns {boolean} - Devuelve true si la recarga fue exitosa
     */
    refreshPage: function () {
        try {
            location.reload();
            return true;
        } catch (e) {
            console.error("Error al recargar la página:", e);
            return false;
        }
    },

    /**
     * Verifica si es posible navegar hacia atrás
     * @returns {boolean} - Devuelve true si hay entradas en el historial para navegar hacia atrás
     */
    canGoBack: function () {
        return window.history.length > 1;
    },

    /**
     * Guarda el estado actual en el sessionStorage para preservarlo
     * @param {string} key - La clave bajo la cual guardar el estado
     * @param {object} state - El objeto de estado a guardar
     * @returns {boolean} - Devuelve true si se guardó correctamente
     */
    saveState: function (key, state) {
        try {
            sessionStorage.setItem(key, JSON.stringify(state));
            return true;
        } catch (e) {
            console.error("Error al guardar estado:", e);
            return false;
        }
    },

    /**
     * Recupera un estado guardado del sessionStorage
     * @param {string} key - La clave bajo la cual está guardado el estado
     * @returns {object|null} - El estado recuperado o null si no existe
     */
    getState: function (key) {
        try {
            const data = sessionStorage.getItem(key);
            return data ? JSON.parse(data) : null;
        } catch (e) {
            console.error("Error al recuperar estado:", e);
            return null;
        }
    },

    /**
     * Elimina un estado guardado del sessionStorage
     * @param {string} key - La clave a eliminar
     * @returns {boolean} - Devuelve true si se eliminó correctamente
     */
    clearState: function (key) {
        try {
            sessionStorage.removeItem(key);
            return true;
        } catch (e) {
            console.error("Error al eliminar estado:", e);
            return false;
        }
    },

    /**
     * Enfoca un elemento por su ID
     * @param {string} elementId - ID del elemento a enfocar
     * @returns {boolean} - Devuelve true si el enfoque fue exitoso
     */
    focusElement: function (elementId) {
        try {
            const element = document.getElementById(elementId);
            if (element) {
                element.focus();
                return true;
            }
            return false;
        } catch (e) {
            console.error("Error al enfocar elemento:", e);
            return false;
        }
    }
};