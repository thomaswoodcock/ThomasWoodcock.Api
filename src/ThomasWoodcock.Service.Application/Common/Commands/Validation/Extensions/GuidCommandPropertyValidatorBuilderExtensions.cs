using System;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="CommandPropertyValidatorBuilder{TCommand,TProperty}" /> that interact with
    ///     <see cref="Guid" /> properties.
    /// </summary>
    internal static class GuidCommandPropertyValidatorBuilderExtensions
    {
        /// <summary>
        ///     Adds a validation rule that ensures that the <see cref="Guid" /> property is not empty.
        /// </summary>
        /// <param name="builder">
        ///     The builder to which the rule will be added.
        /// </param>
        /// <typeparam name="T">
        ///     The type of command whose property will be validated.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="CommandPropertyValidatorBuilder{TCommand,TProperty}" /> to enable further rules to be added.
        /// </returns>
        public static CommandPropertyValidatorBuilder<T, Guid> IsRequired<T>(
            this CommandPropertyValidatorBuilder<T, Guid> builder)
            where T : class, ICommand
        {
            return builder.AddRule(property => property != Guid.Empty,
                propertyName => $"'{propertyName}' must have a value");
        }
    }
}
