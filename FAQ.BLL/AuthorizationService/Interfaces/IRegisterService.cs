using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.SERVICES.AuthorizationService.Interfaces
{
    public interface IRegisterService
    {
        Task<CommonResponse<RegisterViewModel>> Register(RegisterViewModel register);
    }
}