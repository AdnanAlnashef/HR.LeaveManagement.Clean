using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace HR.LeaveManagement.Application.Constracts.Identity
{
    public interface IAuthSerivce
    {
        Task<AuthResponse> Login(AuthRequest authRequest);
        Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
    }
}
