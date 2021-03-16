using ThomasWoodcock.Service.Domain.SharedKernel;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> that interact with
    ///     <see cref="string" /> properties.
    /// </summary>
    internal static class StringCommandPropertyValidatorBuilderExtensions
    {
        /// <summary>
        ///     Adds a validation rule that ensures that the <see cref="string" /> property does not exceed a given
        ///     <paramref name="length" />.
        /// </summary>
        /// <param name="builder">
        ///     The builder to which the rule will be added.
        /// </param>
        /// <param name="length">
        ///     The maximum length.
        /// </param>
        /// <typeparam name="T">
        ///     The type of command whose property will be validated.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> to enable further rules to be added.
        /// </returns>
        public static ICommandPropertyValidatorBuilder<T, string?> HasMaxLength<T>(
            this ICommandPropertyValidatorBuilder<T, string?> builder, int length)
            where T : class, ICommand
        {
            return builder.AddRule(property => (property?.Length ?? int.MinValue) <= length,
                propertyName => $"'{propertyName}' must not be greater than {length} character(s) in length");
        }

        /// <summary>
        ///     Adds a validation rule that ensures that the <see cref="string" /> property does not fall beneath a given
        ///     <paramref name="length" />.
        /// </summary>
        /// <param name="builder">
        ///     The builder to which the rule will be added.
        /// </param>
        /// <param name="length">
        ///     The minimum length.
        /// </param>
        /// <typeparam name="T">
        ///     The type of command whose property will be validated.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> to enable further rules to be added.
        /// </returns>
        public static ICommandPropertyValidatorBuilder<T, string?> HasMinLength<T>(
            this ICommandPropertyValidatorBuilder<T, string?> builder, int length)
            where T : class, ICommand
        {
            return builder.AddRule(property => property != null && property.Length >= length,
                propertyName => $"'{propertyName}' must be at least {length} character(s) in length");
        }

        /// <summary>
        ///     Adds a validation rule that ensures that the <see cref="string" /> property is not null or empty.
        /// </summary>
        /// <param name="builder">
        ///     The builder to which the rule will be added.
        /// </param>
        /// <typeparam name="T">
        ///     The type of command whose property will be validated.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> to enable further rules to be added.
        /// </returns>
        public static ICommandPropertyValidatorBuilder<T, string> IsRequired<T>(
            this ICommandPropertyValidatorBuilder<T, string> builder)
            where T : class, ICommand
        {
            return builder.AddRule(property => !string.IsNullOrEmpty(property),
                propertyName => $"'{propertyName}' must have a value");
        }

        /// <summary>
        ///     Adds a validation rule that ensures that the <see cref="string" /> property is in a valid email address format.
        /// </summary>
        /// <param name="builder">
        ///     The builder to which the rule will be added.
        /// </param>
        /// <typeparam name="T">
        ///     The type of command whose property will be validated.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> to enable further rules to be added.
        /// </returns>
        public static ICommandPropertyValidatorBuilder<T, string> IsValidEmailAddress<T>(
            this ICommandPropertyValidatorBuilder<T, string> builder)
            where T : class, ICommand
        {
            return builder.AddRule(EmailAddress.IsValid,
                propertyName => $"'{propertyName}' must be in a valid email address format");
        }
    }
}
