using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FacturacionPortal.WEB.Helpers
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly LocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtAuthenticationStateProvider(LocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
                return new AuthenticationState(user);

            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);

            if (tokenContent.ValidTo < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("usuarioId");
                return new AuthenticationState(user);
            }

            var claims = new List<Claim>();
            claims.AddRange(tokenContent.Claims);

            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var claims = new List<Claim>();
            claims.AddRange(tokenContent.Claims);

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
