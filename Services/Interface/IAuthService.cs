﻿using FacturacionPortal.WEB.Models.Auth;

namespace FacturacionPortal.WEB.Services.Interface
{
    /// <summary>
    /// Interfaz para servicios de autenticación
    /// </summary>
    public interface IAuthService
    {
        Task<(bool Success, string? Message, LoginResponseDto? Data)> Login(UsuarioLoginDto model);
        Task<(bool Success, string Message, UsuarioPerfilDto? Data)> GetProfile();
        Task<(bool Success, string Message)> Logout();
        Task<bool> IsAuthenticated();
        UsuarioPerfilDto? GetCurrentUser();
    }
}
