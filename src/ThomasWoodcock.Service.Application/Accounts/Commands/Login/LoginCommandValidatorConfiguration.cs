using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Extensions;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.Login
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandValidatorConfiguration{T}" /> used to configure validators for
    ///     <see cref="LoginCommand" /> objects.
    /// </summary>
    internal sealed class LoginCommandValidatorConfiguration : ICommandValidatorConfiguration<LoginCommand>
    {
        /// <inheritdoc />
        public void Configure(ICommandValidatorBuilder<LoginCommand> builder)
        {
            builder.Property(command => command.EmailAddress)
                .IsValidEmailAddress();

            builder.Property(command => command.Password)
                .HasMinLength(15)
                .HasMaxLength(32);
        }
    }
}
