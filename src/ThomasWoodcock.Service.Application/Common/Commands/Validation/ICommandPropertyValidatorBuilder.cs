using System;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Creates a failure message for property validation.
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property that failed validation.
    /// </param>
    internal delegate string PropertyValidationFailureMessageBuilder(string propertyName);

    /// <summary>
    ///     Allows a class to act as a builder for command property validation rules.
    /// </summary>
    /// <typeparam name="TCommand">
    ///     The type of command whose property will be validated.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     The type of property that will be validated.
    /// </typeparam>
    internal interface ICommandPropertyValidatorBuilder<TCommand, out TProperty>
        where TCommand : class, ICommand
    {
        /// <summary>
        ///     Adds a validation rule for the property.
        /// </summary>
        /// <param name="predicate">
        ///     The criteria that the property must meet in order to be considered valid.
        /// </param>
        /// <param name="messageBuilder">
        ///     The message to return if the property is deemed invalid.
        /// </param>
        /// <returns>
        ///     The <see cref="ICommandPropertyValidatorBuilder{TCommand,TProperty}" /> to allow further rules to be added.
        /// </returns>
        ICommandPropertyValidatorBuilder<TCommand, TProperty> AddRule(Predicate<TProperty> predicate,
            PropertyValidationFailureMessageBuilder messageBuilder);
    }
}
