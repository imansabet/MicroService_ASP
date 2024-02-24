using Mango.Services.AuthAPI.Models.DTO;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginRequestDTO> Login (LoginRequestDTO loginRequestDTO);

    }
}
