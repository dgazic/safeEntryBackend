using FluentValidation;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Persistance.Interfaces;

namespace SafeEntry.Core.Validators
{
    public class ActivationAccountValidator : AbstractValidator<ActivationAccountDto>
    {
        private readonly IUserPersistance _userPersistance;
        public ActivationAccountValidator(IUserPersistance userPersistance)
        {
            _userPersistance = userPersistance;

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            RuleFor(e => e).MustAsync(VerifyActivation).WithMessage("Token nije važeći!");
        }

        private async Task<bool> VerifyActivation(ActivationAccountDto activationAccount, CancellationToken token)
        {
            var user = await _userPersistance.GetUserByActivationToken(activationAccount.ActivationToken);

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
