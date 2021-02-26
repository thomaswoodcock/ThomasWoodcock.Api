using ThomasWoodcock.Service.Application.Common.Commands.Validation;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Extensions;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandValidatorConfiguration{T}" /> used to configure validators for
    ///     <see cref="CreateAccountCommand" /> objects.
    /// </summary>
    internal sealed class CreateAccountCommandValidatorConfiguration : ICommandValidatorConfiguration<CreateAccountCommand>
    {
        /// <inheritdoc />
        public void Configure(CommandValidatorBuilder<CreateAccountCommand> builder)
        {
            builder.Property(command => command.Id)
                .IsRequired();

            builder.Property(command => command.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(command => command.EmailAddress)
                .IsValidEmailAddress();

            builder.Property(command => command.Password)
                .HasMinLength(15)
                .HasMaxLength(32);
        }
    }
}
