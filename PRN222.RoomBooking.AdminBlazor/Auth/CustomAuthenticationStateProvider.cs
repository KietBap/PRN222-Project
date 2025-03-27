using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PRN222.RoomBooking.AdminBlazor.ViewModels;
using System.Security.Claims;

namespace PRN222.RoomBooking.AdminBlazor.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _protectedLocalStorage.GetAsync<UserInfo>("userInfo");
            if (result.Success && result.Value != null)
            {
                var session = result.Value;
                if (!session.IsExpired)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, session.FullName),
                        new Claim(ClaimTypes.Role, session.Role)
                    };
                    var identity = new ClaimsIdentity(claims, "CustomAuth");
                    _currentUser = new ClaimsPrincipal(identity);
                }
                else
                {
                    await _protectedLocalStorage.DeleteAsync("userInfo");
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                }
            }
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new AuthenticationState(_currentUser);
    }

    public async Task SignInAsync(ClaimsPrincipal principal)
    {
        _currentUser = principal;

        var name = principal.FindFirst(ClaimTypes.Name)?.Value;
        var role = principal.FindFirst(ClaimTypes.Role)?.Value;
        var session = new UserInfo
        {
            FullName = name,
            Role = role,
            ExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await _protectedLocalStorage.SetAsync("userSession", session);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}