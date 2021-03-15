using System;
using System.Linq.Expressions;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Allows a class to act as a builder for command validators.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of command that the created validators will validate.
    /// </typeparam>
    internal interface ICommandValidatorBuilder<T>
        where T : class, ICommand
    {
        /// <summary>
        ///     Builds a validator.
        /// </summary>
        /// <returns>
        ///     A validator.
        /// </returns>
        ICommandValidator<T> Build();

        /// <summary>
        ///     Adds a validation rule to the builder.
        /// </summary>
        /// <param name="rule">
        ///     The rule to add to the builder.
        /// </param>
        void AddRule(ICommandValidationRule<T> rule);

        /// <summary>
        ///     Provides validation methods for a property on the command.
        /// </summary>
        /// <param name="propertySelector">
        ///     The property to validate.
        /// </param>
        /// <typeparam name="TProperty">
        ///     The type of property that will be validated.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> used to validate the property.
        /// </returns>
        ICommandPropertyValidatorBuilder<T, TProperty> Property<TProperty>(
            Expression<Func<T, TProperty>> propertySelector);
    }
}
