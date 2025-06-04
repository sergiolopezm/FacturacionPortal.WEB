window.authService = {
    clearAuthData: function () {
        localStorage.removeItem('authToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('usuarioId');

        // Opcionalmente mantener recordarme
        // const rememberUser = localStorage.getItem('rememberUser');

        // sessionStorage también por si acaso
        sessionStorage.removeItem('authToken');
        sessionStorage.removeItem('refreshToken');
        sessionStorage.removeItem('usuarioId');

        return true;
    }
};