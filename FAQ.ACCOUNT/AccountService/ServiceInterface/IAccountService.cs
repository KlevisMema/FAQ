using FAQ.SHARED.ResponseTypes;

namespace FAQ.ACCOUNT.AccountService.ServiceInterface
{
    public interface IAccountService
    {
        Task<CommonResponse<string>> ConfirmEmail(string userId);
    }
}