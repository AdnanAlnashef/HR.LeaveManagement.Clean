using Microsoft.AspNetCore.Components.Web;

namespace HR.LeaveManagement.BlazorUI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegiterAsync(string firstName, string lastName, string userName, string email, string password);
        Task Logout();
    }
}
