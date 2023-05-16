using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IActivationAccountService
    {
        Task<ActivationAccountResponse> ActivationAccount(ActivationAccountDto request);

        Task<ActivationAccountResponse> IsActivationTokenValid(String activationToken);
    }
}
