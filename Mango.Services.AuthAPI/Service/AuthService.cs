using Fluent.Infrastructure.FluentModel;
using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext db , UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public Task<LoginRequestDTO> Login(LoginRequestDTO loginRequestDTO)
        {

        }

        public Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {

        }
    }
}
