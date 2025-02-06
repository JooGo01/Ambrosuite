using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.Web.Entities;
using Microsoft.JSInterop;

namespace Ambrosuite.Web.ServicesWeb
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient http, NavigationManager navigation, IJSRuntime jsRuntime)
        {
            _http = http;
            _navigation = navigation;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> Login(LoginViewModel loginData)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                // Guardar sesión en LocalStorage
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "sessionActive", "true");
                return true;
            }

            return false;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var session = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "sessionActive");
            return session == "true";
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "sessionActive");
            _navigation.NavigateTo("/login", forceLoad: true);
        }
    }
}