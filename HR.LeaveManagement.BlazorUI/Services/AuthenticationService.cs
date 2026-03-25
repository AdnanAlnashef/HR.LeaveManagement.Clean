using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client, ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider) 
            : base(client, localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new AuthRequest
                {
                    Email = email,
                    Password = password
                };
                var authenticationRespose = await _client.LoginAsync(authenticationRequest);
                if (authenticationRespose.Token != null)
                {
                    await _localStorageService.SetItemAsync("token", authenticationRespose.Token);

                    await ((ApiAuthenticationStateProvider)
                        _authenticationStateProvider).LoggedIn();

                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegiterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };
            var registrationResponse = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(registrationResponse.UserId))
            {
                return true;
            }
            return false;
        }
    }
}
