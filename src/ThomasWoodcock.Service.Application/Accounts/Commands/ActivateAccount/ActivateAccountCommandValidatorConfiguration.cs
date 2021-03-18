using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Extensions;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandValidatorConfiguration{T}" /> used to configure validators for
    ///     <see cref="ActivateAccountCommand" /> objects.
    /// </summary>
    internal sealed class
        ActivateAccountCommandValidatorConfiguration : ICommandValidatorConfiguration<ActivateAccountCommand>
    {
        /// <inheritdoc />
        public void Configure(ICommandValidatorBuilder<ActivateAccountCommand> builder)
        {
            builder.Property(command => command.AccountId)
                .IsRequired();

            builder.Property(command => command.ActivationKey)
                .IsRequired();
        }
    }
}
